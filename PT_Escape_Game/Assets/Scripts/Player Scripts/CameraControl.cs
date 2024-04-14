using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraControl : MonoBehaviour
{
    private float xRotation = 0f, yRotation = 0f;

    public void MoveCamera(float _mouseSensivity, Transform _playerBody)
    {
        if (FindObjectOfType<Player>().GetCanMove())
        {
            // track the position of arrow axis through time, and multiply it by sensitivity
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;

            if (FindObjectOfType<Player>().interactionsScript.GetCurrentPuzzle() != null)
            {
                // the rotation doesn't go too far away (reduced for Puzzles)
                xRotation = Mathf.Clamp(xRotation, -30f, 30f);
                yRotation = Mathf.Clamp(yRotation, -40f, 40f);

                // apply rotations on the player
                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }
            else
            {
                // the rotation doesn't go too far away
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                // apply rotations on the player
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                _playerBody.Rotate(Vector3.up * mouseX);
            }
        }

    }
}
