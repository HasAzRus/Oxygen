using UnityEngine;

namespace Oxygen
{
    [RequireComponent(typeof(FirstPersonInput), typeof(FirstPersonMotor))]
    [RequireComponent(typeof(PlayerCameraInteraction), typeof(PlayerPointer))]
    public class FirstPersonPlayer : Player
    {
        private FirstPersonMotor _motor;

        private PlayerInteraction _interaction;
        private PlayerPointer _pointer;

        private BaseGrabInteractive _grabInteractive;
        private bool _isGrabbing;

        protected virtual void OnStopInteraction()
        {
            
        }

        protected override void OnGameBeginned()
        {
            base.OnGameBeginned();
            
            GetInput().SetEnabled(true);
            GetInput().SetMode(InputMode.Game);
            
            GetInteraction().SetEnabled(true);
            GetPointer().SetEnabled(true);

            if (GetCamera() is not FirstPersonCamera firstPersonCamera)
            {
                return;
            }
            
            firstPersonCamera.SetMouseLookEnabled(true);
        }

        protected override void OnInitializeComponent()
        {
            base.OnInitializeComponent();
            
            _motor = GetComponent<FirstPersonMotor>();

            _interaction = GetComponent<PlayerInteraction>();
            _pointer = GetComponent<PlayerPointer>();
            
            _interaction.Construct(this);
            _pointer.Construct(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _pointer.TryClear();
        }

        public void Grab(BaseGrabInteractive grabInteractive)
        {
            _grabInteractive = grabInteractive;

            _isGrabbing = true;
        }

        public void StopInteraction()
        {
            if (GetInput() is FirstPersonInput firstPersonInput)
            {
                firstPersonInput.SetConstraint(FirstPersonInputConstraint.All);
            }
            
            OnStopInteraction();
            
            _grabInteractive.StopInteraction(this);
            _grabInteractive = null;
            
            _isGrabbing = false;
        }

        public bool CheckGrabbing()
        {
            return _isGrabbing;
        }

        public BaseGrabInteractive GetGrabInteractive()
        {
            return _grabInteractive;
        }

        public void SetInputConstraint(FirstPersonInputConstraint constraint)
        {
            if (GetInput() is not FirstPersonInput firstPersonInput)
            {
                return;
            }
            
            firstPersonInput.SetConstraint(constraint);
        }

        public FirstPersonMotor GetMotor()
        {
            return _motor;
        }

        public PlayerInteraction GetInteraction()
        {
            return _interaction;
        }

        public PlayerPointer GetPointer()
        {
            return _pointer;
        }
    }
}