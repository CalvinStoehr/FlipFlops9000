using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScene : MonoBehaviour
{
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