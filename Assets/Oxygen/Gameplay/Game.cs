using System;
using System.Collections;
using UnityEngine;

namespace Oxygen
{
	public enum GameEndReason
	{
		Win,
		Loss
	}
	public class Game : Behaviour
	{
		public static event Action<Player> GlobalBeginned;

		public event Action<Player> PlayerConnected;
		public event Action<Player> PlayerDisconnected;

		public event Action Beginned;
		public event Action<GameEndReason> Ended;

		public event Action<bool> PauseChanged;

		public event Action Saving;
		public event Action Loaded;

		[SerializeField] private Player _defaultPlayer;
		[SerializeField] private UserInterface _defaultUserInterface;

		[SerializeField] private Launcher _launcher;

		private bool _isPause;

		private UserInterface _userInterface;

		private void SetPause(bool value)
		{
			_isPause = value;

			OnPauseChanged(value);
			PauseChanged?.Invoke(value);
		}

		private IEnumerator BeginPlayRoutine(Player player)
		{
			yield return null;

			Debug.Log("Игра началась");

			OnBeginned(player);
			
			Beginned?.Invoke();
			GlobalBeginned?.Invoke(player);
		}

		protected virtual void OnPauseChanged(bool value)
		{
			
		}

		protected virtual void OnBeginned(Player player)
		{
			
		}

		protected virtual void OnEnded(GameEndReason reason)
		{
			
		}

		protected virtual void OnPlayerConnected(Player player)
		{
			
		}

		protected virtual void OnPlayerDisconnected(Player player)
		{
			
		}

		protected virtual void OnLevelLoading(ILevel level)
		{
			
		}

		protected virtual void OnLevelLoaded(ILevel level, LoadLevelMode loadLevelMode)
		{
			Player player = null;

			if (loadLevelMode == LoadLevelMode.Single)
			{
				Transform spawnTransform;

				var playerStart = FindFirstObjectByType<PlayerStart>();

				if (playerStart == null)
				{
					Debug.LogError("Ошибка игры: отсутствует старт игрока");

					return;
				}

				spawnTransform = playerStart.transform;
				
				Debug.Log("Спавн игрока");

				player = Instantiate(_defaultPlayer, spawnTransform.position,
					spawnTransform.rotation);

				player.Construct(this);
			}

			if (player != null)
			{
				StartCoroutine(BeginPlayRoutine(player));
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			LevelManager.Instance.Loading += OnLevelLoading;
			LevelManager.Instance.Loaded += OnLevelLoaded;
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			LevelManager.Instance.Loading -= OnLevelLoading;
			LevelManager.Instance.Loaded -= OnLevelLoaded;
		}

		protected override void Start()
		{
			base.Start();

			Debug.Log("Создание экземпляра игры");

			var userInterface = Instantiate(_defaultUserInterface);
			userInterface.Construct(this);

			_userInterface = userInterface;

			Debug.Log("Загрузка стартового уровня");
			LevelManager.Instance.Load(_launcher.GetLevel(), LoadLevelMode.Single);
		}

		public void ConnectPlayer(Player player)
		{
			Debug.Log("Подключение игрока");
			
			OnPlayerConnected(player);
			PlayerConnected?.Invoke(player);
		}

		public void DisconnectPlayer(Player player)
		{
			Debug.Log("Отключение игрока");
			
			OnPlayerDisconnected(player);
			PlayerDisconnected?.Invoke(player);
		}

		public void EndGame(GameEndReason reason)
		{
			Debug.Log($"Игра закончена: {reason}");
			
			OnEnded(reason);
			Ended?.Invoke(reason);
		}

		public void Load()
		{
			DataTransfer.Instance.Load();
			
			Debug.Log("Загрузка данных");

			Loaded?.Invoke();
		}

		public void Save()
		{
			Saving?.Invoke();
			
			Debug.Log("Сохранение данных");

			DataTransfer.Instance.Save();
		}

		public void Pause()
		{
			SetPause(true);
		}

		public void Unpause()
		{
			SetPause(false);
		}

		public bool CheckPause()
		{
			return _isPause;
		}

		public UserInterface GetUserInterface()
		{
			return _userInterface;
		}
	}
}