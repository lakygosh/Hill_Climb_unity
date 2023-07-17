using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Car
{
    public class Player : MonoBehaviour
    {
        private String _username;
        private String _password;
        private bool _join = false;
        [SerializeField] private Rigidbody2D _frontTireRB;
        [SerializeField] private Rigidbody2D _backTireRB;
        [SerializeField] private Rigidbody2D _carRB;
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _rotationSpeed = 300f;

        public CinemachineVirtualCamera playerCameraPrefab;
        private CinemachineVirtualCamera _playerCamera;

        private float _moveInput;

        private void Update()
        {
            _moveInput = Input.GetAxis("Horizontal");
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
    }
}
