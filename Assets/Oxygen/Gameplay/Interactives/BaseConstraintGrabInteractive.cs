using UnityEngine;

namespace Oxygen
{
    public abstract class BaseConstraintGrabInteractive : BaseGrabInteractive
    {
        [SerializeField] private FirstPersonInputConstraint _constraint;

        protected override bool OnInteract(Player player)
        {
            if (player is not FirstPersonPlayer firstPersonPlayer)
            {
                return false;
            }

            firstPersonPlayer.SetInputConstraint(_constraint);

            return base.OnInteract(player);
        }

        protected override void OnStopInteraction(Player player)
        {
            base.OnStopInteraction(player);

            if (player is not FirstPersonPlayer firstPersonPlayer)
            {
                return;
            }
            
            firstPersonPlayer.SetInputConstraint(FirstPersonInputConstraint.None);
        }
    }
}