using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float speed = 12f;            // The speed that the player will move at.

    Vector3 movementInput;                   // The vector to store the direction of the player's movement.
    Vector3 turnInput;

    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    public int playerNumber = 1;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal" + playerNumber);
        float moveV = Input.GetAxis("Vertical" + playerNumber);

        float turnH = Input.GetAxis("TurnH" + playerNumber);
        float turnV = Input.GetAxis("TurnV" + playerNumber);

        Vector3 testInput = new Vector3(0, 0, turnV);

        Debug.Log("Horizontal: " + turnH);
        Debug.Log("Vertical: " + turnV);
        // Move the player around the scene.
        Move(moveH, moveV);

        Turning(turnH, turnV);
    }

    void Move(float h, float v)
    {

        movementInput.Set(v, 0f, h);

        movementInput = movementInput * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movementInput);
    }

    void Turning(float h, float v)
    {
        turnInput.Set(h, 0f, v);

        if (turnInput != Vector3.zero)
        {
            float angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            //Quaternion.LookRotation(turnInput, Vector3.up);
        }



    }
}
