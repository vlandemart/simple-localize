using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Vlandemart.Localization
{
	public class LocalizationManager : MonoBehaviour
	{
		public static LocalizationManager Instance;
		public List<LocalizedString> localizedStrings = new List<LocalizedString>();
		private string currentLanguageKey;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		/// <summary>
		/// Localize the specified key.
		/// </summary>
		/// <returns>Localized string</returns>
		/// <param name="key">Key of the text you want to localize</param>
		public string Localize(string key)
		{
			if (currentLanguageKey == null)
			{
				Debug.LogWarning("Localization for key " + key + " was called but localization is not initialized.");
				return "LOCALIZATION NOT INITIALIZED";
			}
			foreach (LocalizedString locString in localizedStrings)
			{
				if (locString.key == key)
					return locString.text;
			}
			Debug.LogWarning("Key " + key + " was not found in " + currentLanguageKey + " localization.");
			return "NOT LOCALIZED";
		}

		/// <summary>
		/// Deserializes localization file.
		/// </summary>
		/// <param name="languageKey">Language key.</param>
		public void DeserializeLocalization(string languageKey)
		{
			if (currentLanguageKey == languageKey)
			{
				Debug.Log("Localization is already initialized for " + languageKey + " language.");
				return;
			}

			localizedStrings.Clear();
			currentLanguageKey = languageKey;
			
			//Our sample localization path is "Assets/LocalizeEN.json"
			string path = Application.dataPath + "/" + "Localize" + languageKey + ".json";
			if (!File.Exists(path))
			{
				Debug.LogWarning("Tried deserializing " + languageKey + " language, but localize file wasn't found.");
				return;
			}
			StreamReader reader = new StreamReader(path);

			LocalizedString[] strings = JsonHelper.FromJson<LocalizedString>(reader.ReadToEnd());
			for (int i = 0; i < strings.Length; i++)
			{
				localizedStrings.Add(strings[i]);
			}
			Debug.Log("Localization was successfully initialized for " + languageKey + " language.");
		}
	}

	[System.Serializable]
	public class LocalizedString
	{
		public string key;
		public string text;
	}
}