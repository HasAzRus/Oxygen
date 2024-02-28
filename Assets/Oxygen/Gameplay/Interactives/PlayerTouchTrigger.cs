using UnityEngine;

namespace Oxygen
{
    [RequireComponent(typeof(BoxCollider), typeof(InteractiveEventListener))]
    public class PlayerTouchTrigger : BaseTouchInteractive
    {
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = Preferences.TouchTriggerColor;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }

        protected override bool OnInteract(Player player)
        {
            return true;
        }
    }
}