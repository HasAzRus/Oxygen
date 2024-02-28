using UnityEngine;

namespace Oxygen
{
    public class PlayerCameraInteraction : PlayerTriggerInteraction
    {
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;
        
        [SerializeField] private Camera _camera;
        
        private Transform _cameraTransform;
        
        protected virtual Ray GetRay()
        {
            return GetDefaultRay();
        }

        protected override bool OnBeforeCheckInteract(out BaseInteractive interactive)
        {
            interactive = null;
            
            if (!World.Trace(GetRay(), _distance, _layerMask,
                    out var hitInfo))
            {
                return false;
            }

            return hitInfo.collider.TryGetComponent(out interactive);
        }

        protected override void Start()
        {
            base.Start();

            _cameraTransform = _camera.transform;
        }

        protected Ray GetDefaultRay()
        {
            return new Ray(_cameraTransform.position, _cameraTransform.forward);
        }

        protected Camera GetCamera()
        {
            return _camera;
        }

        protected Transform GetCameraTransform()
        {
            return _cameraTransform;
        }

        public void SetDistance(float value)
        {
            _distance = value;
        }
    }
}