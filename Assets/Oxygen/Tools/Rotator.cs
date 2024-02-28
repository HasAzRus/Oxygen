﻿using System;
using UnityEngine;

namespace Oxygen
{
	public class Rotator : Behaviour
	{
		public event Action Rotated;
		public event Action Reversed;

		public event Action<float> ValueChanged;

		[SerializeField] private Vector3 _rotation;

		[SerializeField] private float _speed;

		[SerializeField] private bool _allowToggleWhenRotating;
		[SerializeField] private bool _initialToggleState;

		[SerializeField] private bool _onlyOnce;

		private float _speedMultiplier;

		private Vector3 _targetRotation;
		private Vector3 _moveRotation;

		private Vector3 _initialRotation;

		private bool _isRotating;

		private bool _isToggled;

		protected override void Start()
		{
			base.Start();

			_initialRotation = transform.localEulerAngles;
			_moveRotation = _initialRotation;
			
			_targetRotation = _rotation;

			_isToggled = _initialToggleState;
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();

			if (_isRotating)
			{
				_moveRotation = Vector3.MoveTowards(_moveRotation, _targetRotation, _speed * _speedMultiplier);

				var value = (_moveRotation - _targetRotation).magnitude / (_initialRotation - _rotation).magnitude;

				if (_isToggled)
				{
					ValueChanged?.Invoke(1 - value);
				}
				else
				{
					ValueChanged?.Invoke(value);
				}

				if(_moveRotation == _targetRotation)
				{
					_isRotating = false;

					if (!_isToggled)
					{
						Rotated?.Invoke();

						if (_onlyOnce)
						{
							enabled = false;
						}
					}
					else
					{
						Reversed?.Invoke();
					}
				}
			}

			transform.localEulerAngles = _moveRotation;
		}

		public void StartRotation()
		{
			StartRotation(1f);
		}

		public void StartRotation(float speedMultiplier)
		{
			_isRotating = true;
			_speedMultiplier = speedMultiplier;
		}

		public void StartRotationWithToggle()
		{
			StartRotation(1f);
			Toggle();
		}

		public void StartRotationWithToggle(float speedMultiplier)
		{
			StartRotation(speedMultiplier);
			Toggle();
		}

		public void StopRotation()
		{
			_isRotating = false;
		}

		public void Toggle()
		{
			if (!_allowToggleWhenRotating && _isRotating)
			{
				return;
			}

			_isToggled = !_isToggled;

			_targetRotation = !_isToggled ? _rotation : _initialRotation;
		}
	}
}
