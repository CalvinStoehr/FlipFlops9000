using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

public class LoginScene : MonoBehaviour
{
	[SerializeField] private InputField usernameField;
	[SerializeField] private InputField passwordField;
	[SerializeField] private Text errorText;

	private string loginURL = "http://104.236.29.208/flipflops/login.php";

	public void OnLoginButtonClicked()
	{
		StartCoroutine(SendForm (usernameField.text, passwordField.text));
	}

	IEnumerator SendForm (string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("username", username);
		form.AddField ("password", password);

		WWW www = new WWW (loginURL, form);
		yield return www;

		if (www.text == "Login successful.") {
			SessionInfo.cookies = UnityCookies.ParseCookies (www);
			SceneManager.LoadScene ("mainmenu");
		} else {
			errorText.text = www.text;
		}
	}

	public void OnReturnButtonClicked()
	{
		SceneManager.LoadScene ("register");
	}
}
