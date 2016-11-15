using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class GUIManager : MonoBehaviour
{

	[SerializeField] private GameObject level;
	private LevelManager levelManager;
	
	[SerializeField] private Canvas gameMenu;
	[SerializeField] private Canvas deathMenu;

	// game menu items
	[SerializeField] private Text gameScoreText;

	// death menu items
	[SerializeField] private Text deathScoreText;
	[SerializeField] private Button restartButton;
	[SerializeField] private Button mainMenuButton;

	private string sendScoreURL = "http://104.236.29.208/flipflops/add_run.php";

	private bool showDeathMenu;

	void Start()
	{
		levelManager = level.GetComponent<LevelManager> ();
	}

	public void OnRestartButtonClicked()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void OnMainMenuButtonClicked()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void ShowDeathMenu()
	{
		gameMenu.enabled = false;
		deathMenu.enabled = true;

		deathScoreText.text = "Score: " + Mathf.Round (levelManager.score);

		showDeathMenu = true;

		if (SessionInfo.cookies.ContainsKey("PHPSESSID"))
			StartCoroutine (SendScore ());
	}

	IEnumerator SendScore()
	{
		WWWForm form = new WWWForm();
		form.AddField ("score", Mathf.Round (levelManager.score).ToString());

		WWW www = new WWW (sendScoreURL, form.data, UnityCookies.GetCookieRequestHeader(SessionInfo.cookies));
		yield return www;

		if (www.text == "Send successful.")
			Debug.Log ("YAYY");
		else
			Debug.Log (www.text);
	}

	void OnGUI()
	{
		if (!showDeathMenu) {
			gameScoreText.text = "Score: " + Mathf.Round (levelManager.score);
		}
	}
}
