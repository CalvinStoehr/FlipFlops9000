using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
	[SerializeField] private GameObject accountButton;
	[SerializeField] private GameObject registerButton;
	private string loginFromCookieURL = "http://104.236.29.208/flipflops/login_from_cookie.php";

	void Start()
	{
		if (PlayerPrefs.HasKey ("auth_cookie")) {
			StartCoroutine(SendForm ());
		}
	}

	IEnumerator SendForm ()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("token", PlayerPrefs.GetString("auth_cookie"));

		WWW www = new WWW (loginFromCookieURL, form);
		yield return www;

		if (www.text == "Login successful.") {
			SessionInfo.cookies = UnityCookies.ParseCookies (www);

			accountButton.active = true;
			registerButton.active = false;
		} else {
			Debug.Log (www.text);
		}
	}

	public void ChangeSceneButton(string newScene)
	{
		SceneManager.LoadScene (newScene);
	}

	public void ExitGameButton()
	{
		Application.Quit ();
	}

	public void MasterVolumeSlider(float vol)
	{
		AudioListener.volume = vol;
	}

}