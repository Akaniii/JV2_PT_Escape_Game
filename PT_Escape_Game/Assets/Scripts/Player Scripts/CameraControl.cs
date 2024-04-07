using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraControl : MonoBehaviour
{
    private float xRotation = 0f;

    //public InteractiveElement carriedElement;

    //public GameObject interactiveElementPrefab, interactiveElementsLayer;

    //public LayerMask maskInteractive;
    //

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCamera(float _mouseSensivity, Transform _playerBody)
    {
        // track the position of arrow axis through time, and multiply it by sensitivity
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;

        // the rotation doesn't go too far away
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // apply rotations on the player
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
