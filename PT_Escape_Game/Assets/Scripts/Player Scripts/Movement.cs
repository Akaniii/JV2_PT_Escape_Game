using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;

    public LayerMask groundMask;

    public void PlayerMovements(CharacterController _characterController, Vector3 _velocity, float _speed, float _gravity)
    {
        // check if Player is on the Ground
        CheckGround(_velocity);

        // track the keyboards and controller inputs for movements (Go to Project Settings to change them)
        float moveAxisX = Input.GetAxis("Horizontal");
        float moveAxisZ = Input.GetAxis("Vertical");

        // save in a Vector 3 the directions tracked via inputs
        Vector3 direction = transform.right * moveAxisX + transform.forward * moveAxisZ;

        // apply the movements
        _characterController.Move(direction * _speed * Time.deltaTime);

        // save the gravity on the velocity Y axis 
        _velocity.y += _gravity * Time.deltaTime;

        // apply the gravity in movements
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void CheckGround(Vector3 _velocity)
    {
        float groundDistance = 0.4f;

        // create an invisible sphere that check if there is a collision. Return true if it is
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // check if on ground and velocity is less than 0
        if (isGrounded && _velocity.y < 0)
        {
            // force the player on the ground (better than 0)
            _velocity.y = -2f;
        }
    }
}
