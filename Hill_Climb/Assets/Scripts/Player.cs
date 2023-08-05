using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cinemachine;
using DefaultNamespace;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Car
{
    public class Player : MonoBehaviour
    {
        public PlayerDTO playerData;
        
        [SerializeField] private Rigidbody2D _frontTireRB;
        [SerializeField] private Rigidbody2D _backTireRB;
        [SerializeField] private Rigidbody2D _carRB;
       
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _rotationSpeed = 300f;
        private float _moveInput;
        

        public void Move(InputAction.CallbackContext context)
        {
            _moveInput = -context.ReadValue<float>();
        }

        private void FixedUpdate()
        {
            _frontTireRB.AddTorque(_moveInput * _speed * Time.fixedDeltaTime);
            _backTireRB.AddTorque(_moveInput * _speed * Time.fixedDeltaTime);
            _carRB.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime);
            
        }

        public Vector3 GetPosition() 
        {

                return transform.position;

        }
        
        public void LoadPlayerData(PlayerDTO data)
        {
            playerData = new PlayerDTO(data.Name,data.Surname,data.Id,data.Coins,data.BestScore);
        }
    }
}
