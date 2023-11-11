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
        [SerializeField] private Rigidbody2D _carRB;
       
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 100f;
        private SpeedSlider _speedSlider;
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
        private bool jumpCalledOnce = false;

        
        private CinemachineVirtualCamera _virtualCamera;
        //public static Speedometer speedometar = new Speedometer();
        
        public void Move(InputAction.CallbackContext context)
        {
            _moveInput = -context.ReadValue<float>();
        }

        private void Awake()
        {
            _virtualCamera = GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            if (instance == null) 
            {
                instance = this;   
            }
            //rb = GetComponent<Rigidbody2D>();
            instance = PlayerManager.GetSelectedPlayer();
            StartCoroutine(StartDetectionAfterDelay());

            //_speed = _speedSlider.GetSpeed(); //slajder !!!!
        }

        private void FixedUpdate()
        {
            if (!GameManager.IsGameOver)
            {

                if (Speedometer.speed < 10)
                {
                    _frontTireRB.AddTorque(-_speed);
                    //_backTireRB.AddTorque(-_speed);
                    //_carRB.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime * (-5));
                }
                
                GameObject? nextLava = getLavaGrids();
                if (nextLava!=null && nextLava.transform.position.x - _carRB.transform.position.x + 350f < 10 &&
                    nextLava.transform.position.x - _carRB.transform.position.x + 350f > 0)
                {
                    jumpCalled = false;
                }
                

        }
        }

        public void Jump()
        {
            // Need a ray cast to check if the player is on the ground
            var backTireTransform = _backTireRB.transform;
            RaycastHit2D playerGroundRay = Physics2D.Raycast(new Vector2(backTireTransform.position.x, _backTireRB.position.y - 1.5f*backTireTransform.localScale.y / 2-0.01f), Vector2.down);
            GameObject? nextLava = getLavaGrids();
            if (nextLava != null)
            {
                if (isLavaInView(nextLava))
                {
                    jumpCalled = true;
                    StartCoroutine(delayedJump());
                }
            }
            /*if (playerGroundRay.distance < 0.01f)
            {
                _carRB.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }*/
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
            playerData = new PlayerDTO(data.Name,data.Surname,data.Id,data.Coins,data.BestScore,data.UnlockedCars, data.SelectedCar);
        }
        

        private void Update()
        {
            if (canStartDetection)
            {
                RaycastHit2D playerGroundRay =
                    Physics2D.Raycast(new Vector2(carTransform.position.x, carTransform.position.y - 1.5f),
                        Vector2.down);
                if (!isFlipped && Vector3.Dot(transform.up, Vector3.up) < flipThreshold &&
                    playerGroundRay.distance < 1.5f)
                {
                    //Start the coroutine if the car is flipped
                    StartCoroutine(DelayedFlipReaction());

                }

                if (playerGroundRay.distance > 3.0)
                {
                    //_carRB.AddTorque(_rotationSpeed * Time.fixedDeltaTime * (-0.5f));
                }
            }
            
            if (carTransform.position.y<-25f)
            {
                _virtualCamera.Follow = null;
                _virtualCamera.LookAt = null;
                GameManager.instance.GameOver();
            
            }
            
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
                    if(nextLava.transform.position.x-_carRB.transform.position.x+350f < 0)
                        nextLava = lava;
                    else
                    if(lava.transform.position.x-_carRB.transform.position.x+350f > 0 && (lava.transform.position.x-_carRB.transform.position.x < nextLava.transform.position.x-_carRB.transform.position.x))
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
                    return viewportPoint.x >= 0 && viewportPoint.x <= 0.65 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
                default:
                    return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
            }
        }

        private IEnumerator delayedJump()
        {
            if (!jumpCalledOnce)
            {
                jumpCalledOnce = true;
                yield return new WaitWhile(() => jumpCalled);
                _carRB.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
                jumpCalledOnce = false;
            }
        }
    }
}
