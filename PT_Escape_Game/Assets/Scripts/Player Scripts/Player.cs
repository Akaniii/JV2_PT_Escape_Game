using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;
using OpenCover.Framework.Model;
using System.Linq.Expressions;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float speed, gravity, mouseSensitivity;

    private Vector3 velocityPlayer;

    [SerializeField]
    private Camera mainCamera;

    private bool canMove = true;

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

    public Player GetPlayer()
    {
        return this;
    }

    public void SetTransform(Vector3 newPosition, Vector3 newRotation)
    {
        transform.position = newPosition;
        transform.eulerAngles = newRotation;
    }

    public Camera GetCamera ()
    {
        return mainCamera;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool newBool)
    {
        canMove = newBool;
    }

    public void SetCameraTransform(Vector3 newPosition, Vector3 newRotation)
    {
        mainCamera.transform.localPosition = newPosition;
        mainCamera.transform.localEulerAngles = newRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGame")
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        FindObjectOfType<TimerManager>().SetFinalVictory(true);

        yield return new WaitForSeconds(3);

        StartCoroutine(FindObjectOfType<TimerManager>().GameOver());
    }
}