using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D character;
    private bool jump;
	private bool switchGravity;


    private void Awake()
    {
        character = GetComponent<PlatformerCharacter2D>();
    }


    private void Update()
    {
        if (!jump && !switchGravity)
        {
            // Read the jump input in Update so button presses aren't missed.
            jump = CrossPlatformInputManager.GetButtonDown("Jump");

			// same with gravity switch
			// not really sure how crossplatforminputmanager works-- look at later
			if (!jump)
				switchGravity = Input.GetButtonDown("SwitchGravity");
        }	
    }


    private void FixedUpdate()
    {
        // Pass all parameters to the character control script.
		character.Move (jump);
		jump = false;
		//swithces gravity and checks if grounded and if it should rotate since it only rotates when it should switch gravity
		bool shouldRotate = character.SwitchGravity (switchGravity);
		if (shouldRotate) {
			character.StartRotation ();
		}

		switchGravity = false;
    }
}
