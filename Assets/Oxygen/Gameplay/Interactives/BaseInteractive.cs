using System;
using UnityEngine;

namespace Oxygen
{
	public interface IInteractive
	{
		bool Interact(Player player);
		bool CheckEnabled();
	}

	public abstract class BaseInteractive : Behaviour, IInteractive
	{
		public event Action<Player> Interacting;
		public event Action<Player> Failed;

		[Header("Base Interactive")]

		[SerializeField] private bool _isEnabled;
		
		[SerializeField] private bool _onlyOnce;
		[SerializeField] private bool _destroyAfterInteraction;

		protected abstract bool OnInteract(Player player);
		
		protected virtual void OnFailed(Player player)
		{

		}

		public bool Interact(Player player)
		{
			if (!_isEnabled)
			{
				return false;
			}
			
			if (OnInteract(player))
			{
				Interacting?.Invoke(player);

				if(_onlyOnce)
				{
					_isEnabled = false;
				}

				if (_destroyAfterInteraction)
				{
					Destroy(gameObject);
				}

				return true;
			}

			OnFailed(player);

			Failed?.Invoke(player);

			return false;
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