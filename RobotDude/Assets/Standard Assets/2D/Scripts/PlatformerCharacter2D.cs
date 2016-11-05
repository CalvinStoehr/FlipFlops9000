using System;
using UnityEngine;
using System.Collections.Generic;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	[SerializeField] private float m_RotateSpeed = 90f;

	public bool m_Dead;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
	private bool m_Rotate;
	private float m_RotateTarget = 0;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle") {
			m_Anim.SetBool ("Hit", true);
			m_Dead = true;
		}
	}

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
		


    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

		if (m_Rotate) {
			float rotateAmount = 10 * m_RotateSpeed * Time.deltaTime;
			float currentRotation = transform.rotation.eulerAngles.z;

			if (currentRotation < m_RotateTarget) {
				transform.rotation = Quaternion.Euler(0, Mathf.Min (currentRotation + rotateAmount, m_RotateTarget), Mathf.Min (currentRotation + rotateAmount, m_RotateTarget));
			} else {
				transform.rotation = Quaternion.Euler(0, Mathf.Max (currentRotation - rotateAmount, m_RotateTarget), Mathf.Max (currentRotation - rotateAmount, m_RotateTarget));
			}

			if (transform.rotation.eulerAngles.z == m_RotateTarget)
				m_Rotate = false;
		}
    }


    public void Move(bool jump)
    {
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
			if (Physics2D.gravity.y < 0)
           		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			else
				m_Rigidbody2D.AddForce(new Vector2(0f, -m_JumpForce));

        }
    }

	public void StartRotation()
	{
		m_Rotate = true;
		if (m_RotateTarget == 0)
			m_RotateTarget = 180;
		else
			m_RotateTarget = 0;
	}
}
