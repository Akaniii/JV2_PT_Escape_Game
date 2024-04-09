using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;
using OpenCover.Framework.Model;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float speed, gravity, mouseSensitivity;

    private Vector3 velocityPlayer;

    [SerializeField]
    private Camera mainCamera;

    public Movement movementScript;
    public CameraControl cameraScript;
    public PlayerInteractions interactionsScript;
    public PlayerUI playerUIscript;

    private void Start()
    {
        // lock the cursor on the center on the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        movementScript.PlayerMovements(characterController, velocityPlayer, speed, gravity);
        cameraScript.MoveCamera(mouseSensitivity, transform);

        interactionsScript.InteractionsManager(mainCamera);

        Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.forward * 10, Color.blue);
    }
}