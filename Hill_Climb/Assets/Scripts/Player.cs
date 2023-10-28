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
        private float _moveInput;
        
        private float delayBeforeStart = 2f;
        private bool canStartDetection = false;
        
        private float flipThreshold = -0.8f;
        private float delayBeforeReact = 3f;
        private float jump = 10f;

        private bool isFlipped = false;

        private bool initTorque = false;
        
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
        }

        private void FixedUpdate()
        {
            if (!GameManager.IsGameOver)
            {
                if (_moveInput != 0) {
                    Jump();
                    
                }

                if (Speedometer.speed < 10)
                {
                    _frontTireRB.AddTorque(-_speed);
                    //_backTireRB.AddTorque(-_speed);
                    //_carRB.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime * (-5));
                }
            }

        }

        public void Jump()
        {
            //_carRB.AddForce(new Vector2(_carRB.velocity.x, jump));
            if (IsGrounded())
            {
                _carRB.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            return hit.collider != null;
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
    }
}
