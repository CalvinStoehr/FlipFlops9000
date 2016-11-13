using System;
using UnityEngine;
using System.Collections.Generic;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 800f;                  // Amount of force added when the player jumps.
    [SerializeField] private LayerMask whatIsGround;                  // A mask determining what is ground to the character
	[SerializeField] private float rotateSpeed = 90f;

	[SerializeField] private GameObject level;
	private LevelManager levelManager;

	[SerializeField] private GameObject gui;
	private GUIManager guiManager;

	public bool isDead;
	private bool isGrounded;
	private bool shouldRotate;

    private Transform groundCheck;    // A position marking where to check if the player is grounded.
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private Animator anim;            // Reference to the player's animator component.
    private Rigidbody2D playerRigidbody;
	private float rotateTarget = 0;
   
    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle") {
			anim.SetBool ("Hit", true);
			isDead = true;

			levelManager.StopMoving ();
			guiManager.ShowDeathMenu ();

			Debug.Log ("hit");
			other.enabled = false;
		}
	}

    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
		

	void Start()
	{
		levelManager = level.GetComponent<LevelManager> ();
		guiManager = gui.GetComponent<GUIManager> ();
	}


    private void FixedUpdate()
    {
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
        anim.SetBool("Ground", isGrounded);

        // Set the vertical animation
        anim.SetFloat("vSpeed", playerRigidbody.velocity.y);

		if (shouldRotate) {
			float rotateAmount = 10 * rotateSpeed * Time.deltaTime;
			float currentRotation = transform.rotation.eulerAngles.z;

			if (currentRotation < rotateTarget) {
				transform.rotation = Quaternion.Euler(0, Mathf.Min (currentRotation + rotateAmount, rotateTarget), Mathf.Min (currentRotation + rotateAmount, rotateTarget));
			} else {
				transform.rotation = Quaternion.Euler(0, Mathf.Max (currentRotation - rotateAmount, rotateTarget), Mathf.Max (currentRotation - rotateAmount, rotateTarget));
			}

			if (transform.rotation.eulerAngles.z == rotateTarget)
				shouldRotate = false;
		}
    }


    public void Move(bool jump)
    {
        // If the player should jump...
        if (isGrounded && jump && anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            isGrounded = false;
            anim.SetBool("Ground", false);
			if (Physics2D.gravity.y < 0)
           		playerRigidbody.AddForce(new Vector2(0f, jumpForce));
			else
				playerRigidbody.AddForce(new Vector2(0f, -jumpForce));

        }
    }

	public void StartRotation()
	{
		shouldRotate = true;
		if (rotateTarget == 0)
			rotateTarget = 180;
		else
			rotateTarget = 0;
	}
}
