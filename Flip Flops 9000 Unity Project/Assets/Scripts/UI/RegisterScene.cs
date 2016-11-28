using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class RegisterScene : MonoBehaviour
{
	[SerializeField] private InputField usernameField;
	[SerializeField] private InputField passwordField;
	[SerializeField] private Text errorText;

	private string registerURL = "http://104.236.29.208/flipflops/register.php";

	public void OnRegisterButtonClicked()
	{
		StartCoroutine(SendForm (usernameField.text, passwordField.text));
	}

	IEnumerator SendForm (string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("username", username);
		form.AddField ("password", password);

		WWW www = new WWW (registerURL, form);
		yield return www;

		if (www.text == "Registration successful.") {
			SessionInfo.cookies = UnityCookies.ParseCookies (www);
			Debug.Log ("HERE1");

			if (SessionInfo.cookies.ContainsKey ("auth_cookie")) {
				PlayerPrefs.SetString ("auth_cookie", SessionInfo.cookies ["auth_cookie"]);
				PlayerPrefs.SetString ("username", username);
			}
			Debug.Log ("HERE2");
			SceneManager.LoadScene ("mainmenu");
		} else {
			errorText.text = www.text;
		}
	}

	public void OnLoginButtonClicked()
	{
		SceneManager.LoadScene("login");
	}

	public void OnProceedButtonClicked()
	{
		SceneManager.LoadScene ("mainmenu");
	}
}
