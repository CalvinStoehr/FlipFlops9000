using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	[SerializeField] private GameObject player;
	private PlatformerCharacter2D playerScript;

    public float speed = 0.1f;

    private Vector2 offset = Vector2.zero;
    private Material mat;
    
    // Use this for initialization
	void Start ()
	{
		playerScript = player.GetComponent<PlatformerCharacter2D> ();
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (playerScript)
		//	this.enabled = false;
		
        offset.x += speed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
	}
}
