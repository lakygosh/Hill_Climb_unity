using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Cinemachine;
using DefaultNamespace;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

namespace Car
{
    public class Player : MonoBehaviour
    {
        public PlayerDTO playerData;
        //private Rigidbody2D rb;
        public static Player instance;
        [SerializeField] private Transform carTransform;
        [SerializeField] private Rigidbody2D _frontTireRB;
        [SerializeField] private Rigidbody2D _backTireRB;
        private Rigidbody2D _chasisRB;
        [SerializeField] private Rigidbody2D _carRB;

        private float _speed = 50f;
        public static float _speedLimiter = 1f;
        [SerializeField] private float _rotationSpeed = 100f;
        private SFXController sfxController;
        private float _moveInput;

        private float delayBeforeStart = 2f;
        private bool canStartDetection = false;

        private float flipThreshold = -0.8f;
        private float delayBeforeReact = 3f;
        private float jump = 50f;

        private bool isFlipped = false;

        private bool initTorque = false;
        private bool jumpCalled = false;
        private bool lavaInRange = false;
        private bool isJumping = false;
        GameObject correct;
        GameObject jumpWarning;



        private CinemachineVirtualCamera _virtualCamera;
        //public static Speedometer speedometar = new Speedometer();

        public void Move(InputAction.CallbackContext context)
        {
            _moveInput = -context.ReadValue<float>();
        }

        private void Awake()
        {
            _virtualCamera = GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
            _chasisRB = GameObject.Find("Chasis").GetComponent<Rigidbody2D>();
            Debug.Log(_chasisRB);
        }

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            correct = GameObject.FindGameObjectWithTag("Correct");
            jumpWarning = GameObject.FindGameObjectWithTag("JumpWarning");

            correct.SetActive(false);
            jumpWarning.SetActive(false);
            //rb = GetComponent<Rigidbody2D>();
            instance = PlayerManager.GetSelectedPlayer();
            StartCoroutine(StartDetectionAfterDelay());

            //_speed = _speedSlider.GetSpeed(); //slajder !!!!
        }

        private void FixedUpdate()
        {
            if (!GameManager.IsGameOver)
            {
                Movement();
                /*if (Speedometer.speed < 10)
                {
                    _frontTireRB.AddTorque(-_speed);
                    //_backTireRB.AddTorque(-_speed);
                    //_carRB.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime * (-5));
                }*/

                GameObject? nextLava = getLavaGrids();
                if (nextLava != null && nextLava.transform.position.x - _carRB.transform.position.x + 350f < 10 &&
                    nextLava.transform.position.x - _carRB.transform.position.x + 350f > 0)
                {
                    _carRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                    jumpCalled = false;
                }

                if (nextLava != null && isLavaInView(nextLava))
                {
                    jumpWarning.SetActive(true);
                    //SFXController.instance.JumpNotification();
                }
                else
                { 
                    jumpWarning.SetActive(false);
                }

                if (carTransform.position.y < -25f)
                {
                    _virtualCamera.Follow = null;
                    _virtualCamera.LookAt = null;
                    GameManager.instance.GameOver();

                }
            }
        }

        private void Update()
        {
            if (canStartDetection)
            {
                RaycastHit2D playerGroundRay = Physics2D.Raycast(new Vector2(_backTireRB.transform.position.x, _backTireRB.position.y - 1.5f * _backTireRB.transform.localScale.y / 2 - 0.01f), Vector2.down);
                if (playerGroundRay.distance < 1.5f)
                {
                    _carRB.constraints = RigidbodyConstraints2D.None;
                }
                if (!isFlipped && Vector3.Dot(transform.up, Vector3.up) < flipThreshold &&
                    GameObject.Find("Chasis").GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()))
                {
                    //Start the coroutine if the car is flipped
                    StartCoroutine(DelayedFlipReaction());

                }

                if (playerGroundRay.distance > 3.0)
                {
                    //_carRB.AddTorque(_rotationSpeed * Time.fixedDeltaTime * (-0.5f));
                }

                
            }

           

        }

        private void Movement()
        {
            if (SceneManager.GetActiveScene().name == "OpustanjeSP")
            {
                if (_moveInput == 0)
                {
                    Break();
                }
                else
                {
                    Drive();
                }
            }
            else
            {
                float currentSpeed = _carRB.velocity.magnitude; // Get the current speed

                // Adjust the torque based on the difference between the current speed and the target speed
                float speedDifference = _speed - currentSpeed - 20f - _speedLimiter;
                if (speedDifference > 0)
                {
                    _frontTireRB.angularDrag = 0f;
                    _backTireRB.angularDrag = 0f;
                    _frontTireRB.AddTorque(-_speed*10f*Time.fixedDeltaTime);
                    _backTireRB.AddTorque(-_speed*10f*Time.fixedDeltaTime);
                }
                else
                {
                    _frontTireRB.angularDrag = 3f;
                    _backTireRB.angularDrag = 3f;
                }
            }
        }

        private void Drive()
        {
            float currentSpeed = _carRB.velocity.magnitude; // Get the current speed

            // Adjust the torque based on the difference between the current speed and the target speed
            float speedDifference = _speed - currentSpeed - _speedLimiter;
            if (speedDifference > 0)
            {
                _frontTireRB.AddTorque(_moveInput * _speed * 10f * Time.fixedDeltaTime);
                _backTireRB.AddTorque(_moveInput * _speed * 10f * Time.fixedDeltaTime);
                _carRB.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime * (-5));
                _frontTireRB.angularDrag = 0f;
                _backTireRB.angularDrag = 0f;
            }
        }

        private void Break()
        {
            switch (Difficulty.SELECTED_DIFFICULTY)
            {
                case 0:
                    _frontTireRB.angularDrag = 0.5f;
                    _backTireRB.angularDrag = 0.5f;
                    break;
                case 1:

                    _frontTireRB.angularDrag = 2f;
                    _backTireRB.angularDrag = 2f;
                    break;
                case 2:
                    _frontTireRB.angularDrag = 5f;
                    _backTireRB.angularDrag = 5f;
                    break;
                default:
                    _frontTireRB.angularDrag = 0.5f;
                    _backTireRB.angularDrag = 0.5f;
                    break;
            }

        }

        public void Jump()
        {
            if ((_backTireRB.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>())
                 || _frontTireRB.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>())) && !isJumping)
            {
                GameObject? nextLava = getLavaGrids();
                if (nextLava != null)
                {
                    if (isLavaInView(nextLava))
                    {
                        isJumping = true;
                        jumpCalled = true;

                        correct.SetActive(true);
                        SFXController.instance.Correct();

                        StartCoroutine(delayedJump());

                    }
                }
            }
        }

        public void SetSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }


        public Vector3 GetPosition()
        {

            return transform.position;

        }

        public void LoadPlayerData(PlayerDTO data)
        {
            playerData = new PlayerDTO(data.Name, data.Surname, data.Id, data.Coins, data.BestScore, data.UnlockedCars, data.SelectedCar);
        }


        IEnumerator DelayedFlipReaction()
        {
            isFlipped = true;

            yield return new WaitForSeconds(delayBeforeReact);

            if (Vector3.Dot(transform.up, Vector3.up) > flipThreshold)
            {
                isFlipped = false;
            }

            if (isFlipped)
            {
                GameManager.instance.GameOver();
            }

        }

        IEnumerator StartDetectionAfterDelay()
        {
            yield return new WaitForSeconds(delayBeforeStart);
            canStartDetection = true;
        }

        private GameObject? getLavaGrids()
        {
            GameObject[] lavaGrids = GameObject.FindGameObjectsWithTag("Lava");

            if (lavaGrids.Length > 0)
            {
                GameObject nextLava = lavaGrids[0];

                foreach (var lava in lavaGrids)
                {
                    if (nextLava.transform.position.x - _carRB.transform.position.x + 350f < 0)
                        nextLava = lava;
                    else
                    if (lava.transform.position.x - _carRB.transform.position.x + 350f > 0 && (lava.transform.position.x - _carRB.transform.position.x < nextLava.transform.position.x - _carRB.transform.position.x))
                        nextLava = lava;
                }

                return nextLava;

            }

            return null;
        }

        private bool isLavaInView(GameObject nextLava)
        {
            Vector2 nextLavaPosition = new Vector2(nextLava.transform.position.x + 350f, nextLava.transform.position.y);
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(nextLavaPosition);
            switch (Difficulty.SELECTED_DIFFICULTY)
            {
                case 0:
                    return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
                case 1:
                    return viewportPoint.x >= 0 && viewportPoint.x <= 0.8 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
                case 2:
                    return viewportPoint.x >= 0 && viewportPoint.x <= 0.7 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
                default:
                    return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
            }
        }

        private IEnumerator delayedJump()
        {
            yield return new WaitWhile(() => jumpCalled);
            _carRB.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            SFXController.instance.JumpSound();
            correct.SetActive(false);
            jumpWarning.SetActive(false);

            isJumping = false;
        }
    }
}
