using UnityEngine;
using UnityEngine.Events;

namespace Oxygen
{
    public class PlayerSignalTriggerEventListener : Behaviour
    {
        [SerializeField] private PlayerSignalTrigger _target;

        [SerializeField] private UnityEvent<Player> _onTriggeredEvent;

        private void OnTriggered(Player player)
        {
            _onTriggeredEvent.Invoke(player);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _target.Triggered += OnTriggered;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _target.Triggered -= OnTriggered;
        }
    }
}