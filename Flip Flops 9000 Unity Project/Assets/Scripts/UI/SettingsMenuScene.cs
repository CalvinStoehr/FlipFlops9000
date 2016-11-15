using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsMenuScene : MonoBehaviour
{
	public void ChangeSceneButton(string newScene)
	{
		SceneManager.LoadScene (newScene);
	}
}