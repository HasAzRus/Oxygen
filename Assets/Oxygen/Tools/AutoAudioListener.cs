using UnityEngine;

namespace Oxygen
{
    public class AutoAudioListener : Behaviour
    {
        private AudioListener _audioListener;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_audioListener == null)
            {
                return;
            }

            _audioListener.enabled = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            if (_audioListener == null)
            {
                return;
            }

            _audioListener.enabled = false;
        }

        protected override void Start()
        {
            base.Start();

            _audioListener = GetComponent<AudioListener>();
        }
    }
}