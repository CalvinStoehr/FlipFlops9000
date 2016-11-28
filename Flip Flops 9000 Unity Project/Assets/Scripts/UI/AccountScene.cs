using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AccountScene : MonoBehaviour {

	[SerializeField] private Text accountInfoText;

	void Start ()
	{
		if (PlayerPrefs.HasKey ("username")) {
			accountInfoText.text = "You are logged in as:\n" + PlayerPrefs.GetString ("username");
		} else {
			accountInfoText.text = "There was an error\nloading your username.";
		}
	}

	public void OnLogoutButtonClicked()
	{
		SessionInfo.cookies = new Dictionary<string,string> ();
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene ("mainmenu");
	}

	public void OnReturnButtonClicked()
	{
		SceneManager.LoadScene("mainmenu");
	}
}
