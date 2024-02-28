using UnityEngine;

namespace Oxygen
{
    public class LevelLoader : Behaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private LoadLevelMode _mode;
        
        public void Load()
        {
            LevelManager.Instance.Load(_level, _mode);
        }
    }
}