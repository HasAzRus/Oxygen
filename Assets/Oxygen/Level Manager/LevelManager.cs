using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oxygen
{
	public interface ILevel
	{
		string GetName();
	}

	public enum LoadLevelMode
	{
		Single,
		Additive
	}

	public class LevelManager
	{
		public event Action<ILevel> Loading;
		public event Action<ILevel, LoadLevelMode> Loaded;

		public event Action<ILevel> Unloaded;

		public static LevelManager Instance
		{
			get
			{
				return _instance;
			}
		}

		private static LevelManager _instance;

		private Level _activeLevel;

		public LevelManager()
		{
			if(_instance == null)
			{
				_instance = this;
			}
		}

		private IEnumerator LoadRoutine(Level level, LoadLevelMode loadlLevelMode)
		{
			Loading?.Invoke(level);

			if (_activeLevel != null && loadlLevelMode == LoadLevelMode.Single)
			{
				yield return UnloadRoutine(_activeLevel);
			}

			var loadOperation = SceneManager.LoadSceneAsync(level.GetName(), LoadSceneMode.Additive);
			loadOperation.allowSceneActivation = false;

			while(!loadOperation.isDone) 
			{
				if(loadOperation.progress >= 0.9f)
				{
					loadOperation.allowSceneActivation = true;
				}

				yield return null;
			}

			_activeLevel = level;

            var loadedScene = SceneManager.GetSceneByName(level.GetName());
			SceneManager.SetActiveScene(loadedScene);

			Loaded?.Invoke(level, loadlLevelMode);
		}

		private IEnumerator UnloadRoutine(Level level)
		{
			yield return SceneManager.UnloadSceneAsync(level.GetName());

			Unloaded?.Invoke(level);
		}

		public void Load(Level level, LoadLevelMode loadLevelMode)
		{
			Coroutines.Instance.StartCoroutine(LoadRoutine(level, loadLevelMode));
		}
		
		public void Unload(Level level) 
		{
			Coroutines.Instance.StartCoroutine(UnloadRoutine(level));
		}
	}
}