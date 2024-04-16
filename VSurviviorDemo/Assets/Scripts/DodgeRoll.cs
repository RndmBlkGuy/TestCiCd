using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public float dodgeSpeed = 10f; // Speed of the dodge
    public float dodgeDuration = 0.5f; // How long the dodge lasts
    private bool isDodging = false;
    private Vector3 dodgeDirection;
    private float dodgeTimer;

    void Update()
    {
        if (Input.GetButtonDown("Dodge") && !isDodging)
        {
            // Start dodging
            StartCoroutine(PerformDodge());
        }

        if (isDodging)
        {
            // Apply dodge movement
            transform.Translate(dodgeDirection * dodgeSpeed * Time.deltaTime, Space.World);
            dodgeTimer -= Time.deltaTime;

            if (dodgeTimer <= 0)
            {
                isDodging = false;
            }
        }
    }

    IEnumerator PerformDodge()
    {
        isDodging = true;
        dodgeDirection = -transform.forward; // Dodge in the direction the player is currently facing
        dodgeTimer = dodgeDuration;

        // Optionally add invincibility logic here

        yield return new WaitForSeconds(dodgeDuration);

        // Dodge ends; reset states if needed
        isDodging = false;

        // Optionally end invincibility here
    }
    
}
