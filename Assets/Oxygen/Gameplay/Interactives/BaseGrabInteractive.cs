using System;
using UnityEngine;

namespace Oxygen
{
    public abstract class BaseGrabInteractive : BaseInteractive
    {
        public event Action<Player> Interacted; 

        private bool _isGrabbing;
        
        protected virtual void OnStopInteraction(Player player)
        {
            
        }
        
        protected override bool OnInteract(Player player)
        {
            if (player is not FirstPersonPlayer firstPersonPlayer)
            {
                return false;
            }
            
            _isGrabbing = true;
            firstPersonPlayer.Grab(this);
            
            return true;
        }

        public void StopInteraction(Player player)
        {
            _isGrabbing = false;
            
            OnStopInteraction(player);
            
            Interacted?.Invoke(player);
        }

        public bool CheckGrabbing()
        {
            return _isGrabbing;
        }
    }
}