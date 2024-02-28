using System;
using UnityEngine;
using UnityEngine.UI;

namespace Oxygen
{
    public class FloatToText : Behaviour
    {
        [SerializeField] private Text _text;

        public void SetText(float value)
        {
            _text.text = $"{Math.Round(value, 2)}";
        }
    }
}