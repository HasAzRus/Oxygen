﻿using UnityEngine;

namespace Oxygen
{
    public class LevelUnloader : Behaviour
    {
        [SerializeField] private Level _level;

        public void Unload()
        {
            LevelManager.Instance.Unload(_level);
        }
    }
}