using UnityEngine.UI;
using UnityEngine;

namespace Oxygen
{
	public enum LocalizationTextNameMode
	{
		Field,
		GameObject,
		PrimaryText
	}
	
	public class LocalizationText : Behaviour
	{
		[SerializeField] private string _name;
		[SerializeField] private LocalizationTextNameMode _mode;

		[SerializeField] private string _category;

		[SerializeField] private Text _text;

		private string _primaryText;
		
		private string GetNameByMode(LocalizationTextNameMode mode)
		{
			var result = mode switch
			{
				LocalizationTextNameMode.Field => _name,
				LocalizationTextNameMode.GameObject => gameObject.name,
				LocalizationTextNameMode.PrimaryText => _primaryText,
				_ => string.Empty
			};

			return result;
		}

		private void OnLocalizationRegionChanged(Language language)
		{
			var localizedText = GetLocalizedText(language);

			_text.text = localizedText;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			
			Localization.LanguageChanged += OnLocalizationRegionChanged;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			
			Localization.LanguageChanged -= OnLocalizationRegionChanged;
		}

		protected override void Start()
		{
			base.Start();
			
			Initialize();
			
			UpdateText();
		}

		public string GetLocalizedText(Language language)
		{
			var lName = GetNameByMode(_mode);
			
			if (_category == string.Empty)
			{
				if (Localization.Instance.TryGetText(lName, language, out var text))
				{
					return text;
				}
			}
			else
			{
				if (Localization.Instance.TryGetText(lName, _category, language, out var text))
				{
					return text;
				}
			}

			return string.Empty;
		}

		private string GetLocalizedText()
		{
			var lName = GetNameByMode(_mode);
			
			if (_category == string.Empty)
			{
				if (Localization.Instance.TryGetText(lName, out var text))
				{
					return text;
				}
			}
			else
			{
				if (Localization.Instance.TryGetText(lName, _category, out var text))
				{
					return text;
				}
			}

			return string.Empty;
		}

		public void Initialize()
		{
			_primaryText = _text.text;
		}

		public void UpdateTextByLanguage(Language language)
		{
			var localizedText = GetLocalizedText(language);
			
			_text.text = localizedText;
		}

		public void UpdateText()
		{
			var localizedText = GetLocalizedText();

			_text.text = localizedText;
		}
	}
}
