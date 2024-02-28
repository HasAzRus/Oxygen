using UnityEngine;

namespace Oxygen
{
	public class Starter : Behaviour
	{
		[SerializeField] private Game _defaultGame;
		
		[SerializeField] private Coroutines _defaultCoroutines;
		[SerializeField] private TimeInvoker _defaultTimeInvoker;

		[SerializeField] private GameObject[] _autoSpawnGameObjects;

		[SerializeField] private LocalizationDatabase _localizationDatabase;

		[SerializeField] private Language _initialLanguage;

		[SerializeField] private string _profileName;

		protected override void Start()
		{
			base.Start();

			Debug.Log("Старт системы");

			new LevelManager();
			new DataTransfer(_profileName);
			
			var localization = new Localization(_localizationDatabase);
			localization.ChangeLanguage(_initialLanguage);

			Instantiate(_defaultCoroutines);
			Instantiate(_defaultTimeInvoker);

			foreach (var autoSpawnGameObject in _autoSpawnGameObjects)
			{
				Instantiate(autoSpawnGameObject);
			}
			
			Instantiate(_defaultGame);
		}
	}
}