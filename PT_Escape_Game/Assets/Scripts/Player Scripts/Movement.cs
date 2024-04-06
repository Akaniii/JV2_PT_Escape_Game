using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField]
    private float speed, gravity;

    Vector3 velocityPlayer;

    public Transform groundCheck;

    public float GroundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    private void Update()
    {
        // create an invisible sphere that check if there is a collision. Return true if it is
        isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask);

        // check if on ground and velocity is less than 0
        if (isGrounded && velocityPlayer.y < 0)
        {
            // force the player on the ground (better than 0)
            velocityPlayer.y = -2f;
        }

        // track the keyboards and controller inputs for movements (Go to Project Settings to change them)
        float moveAxisX = Input.GetAxis("Horizontal");
        float moveAxisZ = Input.GetAxis("Vertical");

        // save in a Vector 3 the directions tracked via inputs
        Vector3 direction = transform.right * moveAxisX + transform.forward * moveAxisZ;

        // apply the movements
        characterController.Move(direction * speed * Time.deltaTime);

        // save the gravity on the velocity Y axis 
        velocityPlayer.y += gravity * Time.deltaTime;

        // apply the gravity in movements
        characterController.Move(velocityPlayer * Time.deltaTime);
    }
}
