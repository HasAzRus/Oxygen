using UnityEngine;

namespace Oxygen
{
    public class PlayerInteraction : Behaviour
    {
        private bool _isEnabled;

        private Player _player;
        
        public void Construct(Player player)
        {
            _player = player;
        }

        protected virtual bool OnBeforeCheckInteract(out BaseInteractive interactive)
        {
            interactive = null;
            
            return false;
        }

        public bool CheckInteract(bool allowInteraction)
        {
            if (!_isEnabled)
            {
                return false;
            }

            if (!OnBeforeCheckInteract(out var interactive))
            {
                return false;
            }

            if (interactive is BaseTouchInteractive)
            {
                return false;
            }

            if (!allowInteraction)
            {
                return true;
            }

            return interactive.Interact(_player);
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public bool CheckEnabled()
        {
            return _isEnabled;
        }

        public void SetEnabled(bool value)
        {
            _isEnabled = value;
        }
    }
}