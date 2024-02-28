﻿using UnityEngine;

namespace Oxygen
{
    public enum FirstPersonMotorMode
    {
        Default,
        Spectator
    }
    
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonMotor : Behaviour
    {
        [SerializeField] private float _gravity;
        [SerializeField] private float _speed;

        [SerializeField] private Transform _cameraTransform;

        private Vector3 _moveDirection;
        private Vector3 _inputDirection;

        private Vector3 _direction;

        private Transform _transform;

        private FirstPersonMotorMode _mode;
        
        private CharacterController _characterController;
        
        private void CalculateAirMovement()
        {
            Vector3 direction;
            
            direction = _transform.TransformDirection(_inputDirection);
            
            _moveDirection.x = direction.x * _speed;
            _moveDirection.y -= _gravity * Time.deltaTime;
            _moveDirection.z = direction.z * _speed;
        }

        private void CalculateGroundMovement()
        {
            _moveDirection = _transform.TransformDirection(_inputDirection);
            _moveDirection *= _speed;
        }

        private void CalculateGhostMovement()
        {
            _moveDirection = _cameraTransform.TransformDirection(_inputDirection);
            _moveDirection *= 2f;
        }

        private void CalculateMovement()
        {
            var deltaTime = Time.deltaTime;
            
            if (_mode == FirstPersonMotorMode.Default)
            {
                if (!_characterController.enabled)
                {
                    return;
                }
                
                if (_characterController.isGrounded)
                {
                    CalculateGroundMovement();
                }
                else
                {
                    CalculateAirMovement();
                }
                
                _moveDirection = CalculateAdditionalDirection(_moveDirection);
                
                _characterController.Move(_moveDirection * deltaTime);
            }
            else if (_mode == FirstPersonMotorMode.Spectator)
            {
                CalculateGhostMovement();

                _transform.position += _moveDirection * deltaTime;
            }
        }

        protected virtual void OnModeChanged(FirstPersonMotorMode value)
        {
            _characterController.enabled = value != FirstPersonMotorMode.Spectator;
        }

        protected virtual Vector3 CalculateAdditionalDirection(Vector3 moveDirection)
        {
            return moveDirection;
        }

        protected override void Start()
        {
            base.Start();
            
            _characterController = GetComponent<CharacterController>();

            _transform = transform;
        }

        protected override void Update()
        {
            base.Update();
            
            CalculateMovement();

            if (_inputDirection != Vector3.zero)
            {
                _direction = _inputDirection;
            }
            
            _inputDirection = Vector3.zero;
        }

        public void SetMode(FirstPersonMotorMode value)
        {
            _mode = value;
            
            OnModeChanged(value);
        }

        public FirstPersonMotorMode GetMode()
        {
            return _mode;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }

        public void MoveForward(float value)
        {
            _inputDirection.z = value;
        }

        public void MoveRight(float value)
        {
            _inputDirection.x = value;
        }

        public Vector3 GetVelocity()
        {
            var velocity = _moveDirection;
            velocity.y = 0f;
            
            return velocity;
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public CharacterController GetCharacterController()
        {
            return _characterController;
        }
    }
}