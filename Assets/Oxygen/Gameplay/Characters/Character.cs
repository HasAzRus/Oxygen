using System;
using UnityEngine;

namespace Oxygen
{
	public class Character : Behaviour,
		IDamageReceiver,
		IKillable
	{
		public event Action<GameObject, float> DamageApplied;
		public event Action<GameObject> Killed;

		public event Action<float> HealthChanged;
		
		[Header("Character")]

		[SerializeField] private bool _allowDamageReceive;

		[SerializeField] private float _maxHealth;
		[SerializeField] private bool _applyMaxHealthInStart;

		private bool _isDead;

		private float _health;

		protected virtual void OnDamageApplied(GameObject caller,  float damage)
		{
			if (_allowDamageReceive)
			{
				if (_health - damage > 0f)
				{
					SetHealth(_health - damage);
				}
				else
				{
					SetHealth(0);

					Kill(caller);
				}
			}
		}

		protected virtual void OnKill(GameObject caller) 
		{ 

		}

		protected override void Start()
		{
			base.Start();

			if (_applyMaxHealthInStart)
			{
				SetHealth(_maxHealth);
			}
		}

		public void ApplyDamage(GameObject caller, float damage)
		{
			OnDamageApplied(caller, damage);
			DamageApplied?.Invoke(caller, damage);
		}

		public void Kill(GameObject caller)
		{
			if(CheckDead())
			{
				return;
			}

			_isDead = true;

			OnKill(caller);
			Killed?.Invoke(caller);
		}

		public void SetHealth(float value)
		{
			_health = value;

			HealthChanged?.Invoke(value);
		}

		public bool AddHealth(float amount)
		{
			if(_health == _maxHealth)
			{
				return false;
			}

			if(_health + amount < _maxHealth)
			{
				SetHealth(_health + amount);
			}
			else
			{
				SetHealth(_maxHealth);
			}

			return true;
		}

		public float GetMaxHealth()
		{
			return _maxHealth;
		}

		public float GetHealth()
		{
			return _health;
		}

		public bool CheckDead()
		{
			return _isDead;
		}

		public void SetAllowDamageReceive(bool value)
		{
			_allowDamageReceive = value;
		}
	}
}