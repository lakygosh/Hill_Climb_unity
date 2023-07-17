using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Car
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private String username;
        private String password;
        [SerializeField] private Rigidbody2D _frontTireRB;
        [SerializeField] private Rigidbody2D _backTireRB;
        [SerializeField] private Rigidbody2D _carRB;
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _rotationSpeed = 300f;

        public CinemachineVirtualCamera playerCameraPrefab;
        private CinemachineVirtualCamera _playerCamera;

        private float _moveInput;

        public override void OnNetworkSpawn()
        {
            _playerCamera = Instantiate(playerCameraPrefab);
            _playerCamera.Follow = this.transform;
            _playerCamera.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!IsOwner)
            {
                return;
            }
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
