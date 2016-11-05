using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private bool m_SwitchGravity;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump && !m_SwitchGravity)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

				// same with gravity switch
				// not really sure how crossplatforminputmanager works-- look at later
				if (!m_Jump)
					m_SwitchGravity = Input.GetButtonDown("SwitchGravity");
            }	
        }


        private void FixedUpdate()
        {
            // Pass all parameters to the character control script.
			m_Character.Move (m_Jump);
			m_Jump = false;

			if (m_SwitchGravity) {
				Physics2D.gravity = -Physics2D.gravity;
				m_Character.StartRotation ();
			}

			m_SwitchGravity = false;
        }
    }
}
