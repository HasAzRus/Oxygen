using UnityEngine;

namespace Oxygen
{
    public class PlayerCameraMode : Behaviour
    {
        [SerializeField] private Camera _camera;

        public void ToggleTo(Player player)
        {
            player.GetCamera().ToggleTo(_camera);
        }

        public void ToggleToDefault(Player player)
        {
            player.GetCamera().ToggleToDefault();
        }
    }
}