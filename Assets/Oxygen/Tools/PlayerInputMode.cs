using UnityEngine;

namespace Oxygen
{
    public class PlayerInputMode : Behaviour
    {
        [SerializeField] private InputMode _mode;
        [SerializeField] private bool _isEnabled;

        public void SetInput(Player player)
        {
            player.GetInput().SetMode(_mode);
        }

        public void SetEnabled(Player player)
        {
            player.GetInput().SetEnabled(_isEnabled);
        }
    }
}