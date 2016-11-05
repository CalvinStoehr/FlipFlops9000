using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
	}

	void OnGUI()
	{
		if (!showDeathMenu) {
			gameScoreText.text = "Score: " + Mathf.Round (levelManager.score);
		}
	}
}
