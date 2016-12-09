using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

	[SerializeField] private float playerSpeed = 100f;
	[SerializeField] private float playerAcceleration = 10f;
	[SerializeField] private float scoreMultiplier = 5f;

	public float score;

	// Update is called once per frame
	void Update ()
	{
		playerSpeed += playerAcceleration * Time.deltaTime;

		float movementMultiplier = Mathf.Log (playerSpeed) * Time.deltaTime;
		Vector3 movementAmount = new Vector3 (1f, 0f, 0f) * movementMultiplier;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).transform.position -= movementAmount;
		}

		score += movementMultiplier * scoreMultiplier;
	}

	public void StopMoving()
	{
		this.enabled = false;
	}
}
