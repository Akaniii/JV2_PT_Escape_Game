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

    // hold an Element = true or false
    private bool holdSomething;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject handPosition, carriedElement, interactiveElementsLayer;

    public LayerMask maskInteractive;

    [SerializeField]
    private TextMeshProUGUI interactInputInfo, dropInputInfo, errorNotification;

    public Movement movementScript;
    public CameraControl cameraScript;

    private void Start()
    {
        // lock the cursor on the center on the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        movementScript.PlayerMovements(characterController, velocityPlayer, speed, gravity);
        cameraScript.MoveCamera(mouseSensitivity, transform);

        PlayerInteractions();

        Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.forward * 10, Color.blue);
    }

    private void PlayerInteractions()
    {
        // if raycast centered with camera hit an interactive element (mask interactive), make UI appears and enable interaction
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hitInfo, 2, maskInteractive))
        {
            // if raycast detect element movable can be picked OR interactiveElement (not movables)
            if (CheckInteractionsRaycast(hitInfo))
            {
                // show input to interact
                ShowUIInteractionInput(hitInfo, true);

                if ((hitInfo.collider.GetComponent<MovableElement>() != null && !holdSomething) || hitInfo.collider.GetComponent<MovableElement>() == null)
                {
                    if (Input.GetKey(KeyCode.F))
                    {
                        hitInfo.collider.GetComponent<InteractiveElement>().Interact();
                        ShowUIInteractionInput(hitInfo, false);
                    }
                }
                else
                {
                    errorNotification.gameObject.SetActive(true);
                    errorNotification.text = "You cannot Interact with this for now.";

                    HideErrorNotification(errorNotification);
                }
            }
            else
            {
                // hide input to interact if collide with something not Interactible
                ShowUIInteractionInput(hitInfo, false);
            }
        }
        else
        {
            // hide input to interact if no collide on raycast
            ShowUIInteractionInput(hitInfo, false);
        }

        DropCarriedElement();

        ShowUIDropElement();

        RotateCarriedElement();
    }

    private void RotateCarriedElement ()
    {
        if(holdSomething)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                carriedElement.transform.Rotate(0f, -1f, 0f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                carriedElement.transform.Rotate(0f, 1f, 0f);
            }
        }
    }

    public GameObject GetCarriedElement()
    {
        return handPosition;
    }

    public void SetCarriedElement(GameObject _carriedElement)
    {
        carriedElement = _carriedElement; 
    }

    public void DropCarriedElement()
    {
        if (holdSomething && Input.GetKey(KeyCode.X))
        {
            holdSomething = false;

            carriedElement.GetComponent<MovableElement>().SetCanBePicked(true);
            carriedElement.GetComponent<Rigidbody>().isKinematic = false;
            carriedElement.GetComponent<Collider>().enabled = true;

            carriedElement.transform.parent = null;

            carriedElement = null;
        }
    }

    public void SetHoldSomething(bool newBool)
    {
        holdSomething = newBool;
    } 

    public void ShowUIInteractionInput(RaycastHit _hitInfo, bool catchInteraction)
    {
        // if raycast detect element movable can be picked OR interactiveElement (not movables)
        if (catchInteraction)
        {
            // show text to let the player know what action he can make with the interactive element
            interactInputInfo.gameObject.SetActive(true);

            interactInputInfo.text = "F - " + _hitInfo.collider.GetComponent<InteractiveElement>().GetAction() + " " + _hitInfo.collider.GetComponent<InteractiveElement>().GetName();
        }
        else
        {
            interactInputInfo.gameObject.SetActive(false);
        }
    }

    private void ShowUIDropElement()
    {
        if (holdSomething)
        {
            dropInputInfo.gameObject.SetActive(true);
            dropInputInfo.text = "X - Drop " + carriedElement.GetComponent<InteractiveElement>().GetName();
        }
        else
        {
            dropInputInfo.gameObject.SetActive(false);
        }
    }

    public bool CheckInteractionsRaycast(RaycastHit _hitInfo)
    {
        if (_hitInfo.collider.GetComponent<MovableElement>() != null)
        {
            if (_hitInfo.collider.GetComponent<MovableElement>().GetCanBePicked())
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        else if (_hitInfo.collider.GetComponent<InteractiveElement>() != null)
        {
            return true;
        }
        else 
        { 
            return false; 
        }     
    }

    public IEnumerator HideErrorNotification(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds (3);
        text.gameObject.SetActive (false);
    }

}