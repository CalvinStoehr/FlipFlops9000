using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
	public void ChangeSceneButton(int newScene)
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
