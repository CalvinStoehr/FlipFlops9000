using UnityEngine;
using System.Collections;

public class InteractablesSpawner : MonoBehaviour {

	[SerializeField] private GameObject player;
	private PlatformerCharacter2D playerScript;

	// Use this for initialization
	void Start ()
	{
		playerScript = player.GetComponent<PlatformerCharacter2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript.m_Dead)
			this.enabled = false;
		
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).transform.position -= new Vector3(3f, 0f, 0f) * Time.deltaTime;
		}
	}
}
