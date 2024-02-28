using System;

namespace Oxygen
{
    public class PlayerSignalTrigger : Behaviour
    {
        public event Action<Player> Triggered;
        
        private Player _player;
        
        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public void SetTrigger()
        {
            Triggered?.Invoke(_player);
        }
    }
}