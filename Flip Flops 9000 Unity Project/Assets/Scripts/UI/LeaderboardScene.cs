using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardScene : MonoBehaviour
{
	[SerializeField] private GameObject labels;
	[SerializeField] private Text namesText;
	[SerializeField] private Text scoresText;
	[SerializeField] private Text loadingText;
	[SerializeField] private Text errorText;

	private string registerURL = "http://104.236.29.208/flipflops/fetch_scores.php";

	void Start()
	{
		StartCoroutine(SendForm ());
	}

	IEnumerator SendForm ()
	{
		WWW www = new WWW (registerURL);
		yield return www;

		loadingText.enabled = false;

		string[] lines = www.text.Split(new string[] {"\n", "\r\n"}, StringSplitOptions.None);
		if (lines[0] == "Success!") {
			if (lines.Length == 3) {
				loadingText.text = "No scores in database yet.";
				loadingText.enabled = true;
			} else {
				string names = "";
				string scores = "";

				bool onNames = true;
				for (int i = 1; i < lines.Length; i++) {
					if (lines [i] == "$$||$$") {
						onNames = false;
						continue;
					}

					if (onNames)
						names += lines [i] + "\n";
					else
						scores += lines [i] + "\n";
				}

				labels.active = true;
				namesText.text = names;
				scoresText.text = scores;
			}
		} else {
			errorText.text = www.text != "" ? www.text : "Could not load database.";
		}
	}

	public void OnReturnButtonClicked()
	{
		SceneManager.LoadScene("mainmenu");
	}
}
