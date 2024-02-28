using System;
using UnityEngine;

namespace Oxygen
{
    public class TimeInvoker : Behaviour
    {
        public static TimeInvoker Instance => _instance;

        public event Action<float> UpdateTimeTicked;
        public event Action<float> UpdateTimeUnscaledTicked;

        public event Action OneSecondTicked;
        public event Action OneSecondUnscaledTicked;

        private static TimeInvoker _instance;

        private float _oneSecondTimer;
        private float _oneSecondUnscaledTimer;

        protected override void Awake()
		{
			base.Awake();

            if(_instance != null)
			{
                Destroy(gameObject);
			}

            _instance = this;
		}

		protected override void Update()
		{
			base.Update();

            var deltaTime = Time.deltaTime;
            UpdateTimeTicked?.Invoke(deltaTime);

            _oneSecondTimer += deltaTime;

            if(_oneSecondTimer >= 1f)
			{
                _oneSecondTimer -= 1f;
                OneSecondTicked?.Invoke();
			}

            var unscaledDeltaTime = Time.unscaledDeltaTime;
            UpdateTimeUnscaledTicked?.Invoke(unscaledDeltaTime);

            _oneSecondUnscaledTimer += unscaledDeltaTime;

            if(_oneSecondUnscaledTimer >= 1f)
			{
                _oneSecondUnscaledTimer -= 1f;
                OneSecondUnscaledTicked?.Invoke();
			}
		}
	}
}
