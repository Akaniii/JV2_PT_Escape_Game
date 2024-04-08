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
    private GameObject carriedElement, interactiveElementsLayer;

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

        //Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.forward * 10, Color.blue);
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
                ManageUI(hitInfo, true);

                if ((hitInfo.collider.GetComponent<MovableElement>() != null && !holdSomething) || hitInfo.collider.GetComponent<MovableElement>() == null)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        hitInfo.collider.GetComponent<InteractiveElement>().Interact();
                        ManageUI(hitInfo, false);
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
                // hide input to interact
                ManageUI(hitInfo, false);
            }
        }

        DropCarriedElement();
    }

    public GameObject GetCarriedElement()
    {
        return carriedElement;
    }
    public void SetCarriedElement(GameObject objectPicked)
    {
        carriedElement = objectPicked;
    }

    public void DropCarriedElement()
    {
        if (holdSomething && Input.GetKey(KeyCode.X))
        {
            holdSomething = false;

            GameObject savedCarriedElement = carriedElement;
            savedCarriedElement.GetComponent<MovableElement>().SetCanBePicked(true);
            savedCarriedElement.GetComponent<Rigidbody>().isKinematic = false;

            carriedElement = null;

            GameObject newElement = Instantiate(savedCarriedElement, carriedElement.transform.position, carriedElement.transform.rotation, interactiveElementsLayer.transform);

            //carriedElement.DropObject(carriedElement, interactiveElementsLayer);

            //newElement.GetComponent<InteractiveElement>().SetName(carriedElement.GetComponent<InteractiveElement>().GetName());
            //newElement.GetComponent<MeshRenderer>().material = carriedElement.GetComponent<MeshRenderer>().material;
            //newElement.GetComponent<MeshFilter>().mesh = carriedElement.GetComponent<MeshFilter>().mesh;

            //carriedElement.SetName(null);
            //carriedElement.GetComponent<MeshRenderer>().material = null;
            //carriedElement.GetComponent<MeshFilter>().mesh = null;


            //carriedElement.GetComponent<MeshRenderer>().material = null;
            //carriedElement.GetComponent<MeshFilter>().mesh = null;


            //Instantiate(interactiveElementPrefab, carriedElement.transform);
        }
    }

        public void SetHoldSomething(bool newBool)
    {
        holdSomething = newBool;
    } 

    public void ManageUI(RaycastHit _hitInfo, bool catchInteraction)
    {
        // if raycast detect element movable can be picked OR interactiveElement (not movables)
        if (catchInteraction)
        {
            // show text to let the player know what action he can make with the interactive element
            interactInputInfo.gameObject.SetActive(true);

            interactInputInfo.text = "E - " + _hitInfo.collider.GetComponent<InteractiveElement>().GetAction() + " " + _hitInfo.collider.GetComponent<InteractiveElement>().GetName();
        }
        else
        {
            interactInputInfo.gameObject.SetActive(false);
        }

        if (holdSomething)
        {
            dropInputInfo.gameObject.SetActive(true);
            dropInputInfo.text = "X - Drop " + _hitInfo.collider.GetComponent<InteractiveElement>().GetName();
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