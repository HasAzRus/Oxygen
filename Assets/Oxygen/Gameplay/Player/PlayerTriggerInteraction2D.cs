using UnityEngine;

namespace Oxygen
{
    public class PlayerTriggerInteraction2D : PlayerInteraction
    {
        [SerializeField] private bool _allowTouchInteractive;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
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

        protected override void OnTriggerExit2D(Collider2D other)
        {
            base.OnTriggerExit2D(other);
            
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