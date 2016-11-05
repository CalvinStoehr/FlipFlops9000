using UnityEngine;
using System.Collections;

public class BGScaler : MonoBehaviour {

	[SerializeField] private GameObject player;
	private PlatformerCharacter2D playerScript;

	// Use this for initialization
	void Start ()
	{
		playerScript = player.GetComponent<PlatformerCharacter2D> ();

        var height = Camera.main.orthographicSize * 2f;
        var width = height * Screen.width / Screen.height;

        if(gameObject.name == "Background"){
            transform.localScale = new Vector3(width, height, 0);
        }
        else{
            transform.localScale = new Vector3(width + 3f, 5, 0);
        }
	}

	void Update()
	{
		if (playerScript.m_Dead)
			this.enabled = false;
	}
}
