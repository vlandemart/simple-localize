using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Don't forget to include namespace
using Vlandemart.Localization;

public class Dialogue : MonoBehaviour
{
	private void Start()
	{
		string reply;
		//At first - we load needed localization
		LocalizationManager.Instance.DeserializeLocalization("EN");
		//Then - use method Localize() to translate key into currently loaded language
		reply = LocalizationManager.Instance.Localize("welcome");
		Debug.Log(reply);

		//The same for any other localization you have in project
		LocalizationManager.Instance.DeserializeLocalization("RU");
		reply = LocalizationManager.Instance.Localize("welcome");
		Debug.Log(reply);
	}
}
