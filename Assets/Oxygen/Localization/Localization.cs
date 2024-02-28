using System;
using UnityEngine;

namespace Oxygen
{
	public class Localization
	{
		public static event Action<Language> LanguageChanged;

		public static Localization Instance
		{
			get
			{
				return _instance;
			}
		}

		private static Localization _instance;

		private Language _currentLanguage;

		private readonly LocalizationDatabase _database;

		public Localization(LocalizationDatabase database)
		{
			_database = database;

			_instance = this;
		}
		
		private bool TryGetItem(string name, out LocalizationItem item)
		{
			return _database.TryGetItem(name, out item);
		}

		private bool TryGetTextByIndex(string name, int index, Language language, out string text)
		{
			text = string.Empty;

			if (!TryGetItem(name, out LocalizationItem item))
			{
				return false;
			}

			return item.TryGetValue(index, language, out text);
		}

		private bool TryGetTextByIndex(string name, string category, int index, Language language, out string text)
		{
			text = string.Empty;

			if (!_database.TryGetCategory(category, out LocalizationCategory categoryItem))
			{
				return false;
			}

			if (!categoryItem.TryGetItem(name, out LocalizationItem item))
			{
				return false;
			}

			return item.TryGetValue(index, language, out text);
		}

		public bool TryGetText(string name, out string text)
		{
			return TryGetText(name, _currentLanguage, out text);
		}

		public bool TryGetText(string name, Language language, out string text)
		{
			return TryGetTextByIndex(name, 0, language, out text);
		}

		public bool TryGetText(string name, string category, out string text) 
		{
			return TryGetText(name, category, _currentLanguage, out text);
		}

		public bool TryGetText(string name, string category, Language language, out string text)
		{
			return TryGetTextByIndex(name, category, 0, language, out text);
		}

		public bool TryGetRandomText(string name, Language language, out string text)
		{
			text = string.Empty;

			if (!TryGetItem(name, out LocalizationItem item))
			{
				return false;
			}

			return item.TryGetRandomValue(language, out text);
		}

		public bool TryGetRandomText(string name, out string text)
		{
			return TryGetRandomText(name, _currentLanguage, out text);
		}

		public bool TryGetRandomText(string name, string category, Language language, out string text)
		{
			text = string.Empty;

			if (!_database.TryGetCategory(category, out LocalizationCategory categoryItem))
			{
				return false;
			}

			if (!categoryItem.TryGetItem(name, out LocalizationItem item))
			{
				return false;
			}

			return item.TryGetRandomValue(language, out text);
		}

		public bool TryGetRandomText(string name, string category, out string text)
		{
			return TryGetRandomText(name, category, _currentLanguage, out text);
		}

		public bool ChangeLanguage(Language language)
		{
			Debug.Log($"Язык был изменен на {language}");

			_currentLanguage = language;
			LanguageChanged?.Invoke(language);

			return true;
		}
	}
}
