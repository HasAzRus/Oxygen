using UnityEngine;

namespace Oxygen
{
    public class PlayerTriggerInteraction : PlayerInteraction
    {
        [SerializeField] private bool _allowTouchInteractive;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            if (!CheckEnabled())
            {
                return;
            }

            if (!_allowTouchInteractive)
            {
                return;
            }
            
            if (!other.TryGetComponent(out BaseTouchInteractive touchInteractive))
            {
                return;
            }

            touchInteractive.Interact(GetPlayer());
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            
            if (!CheckEnabled())
            {
                return;
            }
            
            if (!_allowTouchInteractive)
            {
                return;
            }
            
            if (!other.TryGetComponent(out BaseTouchInteractive touchInteractive))
            {
                return;
            }

            touchInteractive.StopInteraction(GetPlayer());
        }
    }
}