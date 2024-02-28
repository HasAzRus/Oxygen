using UnityEngine;

namespace Oxygen
{
    public enum FirstPersonInputConstraint
    {
        None,
        OnlyMovement,
        OnlyRotation,
        All
    }
    
    public class FirstPersonInput : PlayerInput
    {
        [SerializeField] private bool _allowToggleToFreeCamera;
        
        [SerializeField] private float _sensitive;

        private FirstPersonInputConstraint _constraint;

        private void Move(FirstPersonMotor motor)
        {
            motor.MoveForward(Input.GetAxis("Vertical"));
            motor.MoveRight(Input.GetAxis("Horizontal"));
        }

        private void Rotate(FirstPersonCamera firstPersonCamera)
        {
            firstPersonCamera.LookAt(-Input.GetAxis("Mouse Y") * _sensitive);
            firstPersonCamera.Turn(Input.GetAxis("Mouse X") * _sensitive);
        }

        protected override void OnGameInput()
        {
            base.OnGameInput();

            if (GetPlayer() is not FirstPersonPlayer firstPersonPlayer)
            {
                return;
            }

            var motor = firstPersonPlayer.GetMotor();

            var disableMovement = _constraint is FirstPersonInputConstraint.All or FirstPersonInputConstraint.OnlyMovement;
            
            if (!disableMovement)
            {
                Move(motor);
            }

            if (_allowToggleToFreeCamera)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    var mode = motor.GetMode() == FirstPersonMotorMode.Default
                        ? FirstPersonMotorMode.Spectator
                        : FirstPersonMotorMode.Default;

                    motor.SetMode(mode);
                }
            }

            var camera = firstPersonPlayer.GetCamera();

            if (camera is not FirstPersonCamera firstPersonCamera)
            {
                return;
            }

            var disableRotation = _constraint is FirstPersonInputConstraint.All or FirstPersonInputConstraint.OnlyRotation;
            
            if (!disableRotation)
            {
                Rotate(firstPersonCamera);
            }

            var interaction = firstPersonPlayer.GetInteraction();

            if (Input.GetButtonDown("Interact"))
            {
                if (!interaction.CheckInteract(true))
                {
                    Debug.Log("Ошибка взаимодействия");
                }
            }

            if (Input.GetButtonUp("Interact"))
            {
                if (firstPersonPlayer.CheckGrabbing())
                {
                    firstPersonPlayer.StopInteraction();
                }
            }
        }

        public FirstPersonInputConstraint GetConstraint()
        {
            return _constraint;
        }
        
        public void SetConstraint(FirstPersonInputConstraint value)
        {
            _constraint = value;
        }
    }
}