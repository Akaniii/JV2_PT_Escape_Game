using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float speed, gravity, mouseSensitivity;

    private Vector3 velocityPlayer;

    private bool holdSomething;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject carriedElement;

    public LayerMask maskInteractive;

    [SerializeField]
    private TextMeshProUGUI interactInputInfo, dropInputInfo;

    public Movement movementScript;
    public CameraControl cameraScript;

    // carry an Element = true or false
    private bool isCarrying;

    private void Start()
    {
        // lock the cursor on the center on the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        movementScript.PlayerMovements(characterController, velocityPlayer, speed, gravity);
        cameraScript.MoveCamera(mouseSensitivity, transform);

        //Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.forward * 10, Color.blue);
    }

    private void PlayerInteractions()
    {
        // if raycast centered with camera hit an interactive element (mask interactive), make UI appears and enable interaction
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hitInfo, 2, maskInteractive))
        {
            // show text to let the player know what action he can make with the interactive element
            interactInputInfo.gameObject.SetActive(true);

            interactInputInfo.text = "E - " + hitInfo.collider.GetComponent<InteractiveElement>().GetAction() + " " + hitInfo.collider.GetComponent<InteractiveElement>().GetName();

            //        if (Input.GetKey(KeyCode.E) && !isCarrying)
            //        {
            //            //hitInfo.collider.GetComponent<InteractiveElement>().PickObject(carriedElement);
            //            dropInput.gameObject.SetActive(true);
            //            dropInput.text = "X - Drop " + hitInfo.collider.GetComponent<InteractiveElement>().GetName();

            //            isCarrying = true;
            //        }
            //    }
            //    else
            //    {
            //        interactiveInput.gameObject.SetActive(false);
            //    }

            //    if (!isCarrying)
            //    {
            //        dropInput.gameObject.SetActive(false);
            //    }

            //    if (isCarrying && Input.GetKey(KeyCode.X))
            //    {
            //        //carriedElement.SetName(null);
            //        isCarrying = false;

            //        //carriedElement.DropObject(carriedElement, interactiveElementsLayer);

            //        GameObject newElement = Instantiate(interactiveElementPrefab, carriedElement.transform.position, carriedElement.transform.rotation, interactiveElementsLayer.transform);

            //        newElement.GetComponent<InteractiveElement>().SetName(carriedElement.GetComponent<InteractiveElement>().GetName());
            //        newElement.GetComponent<MeshRenderer>().material = carriedElement.GetComponent<MeshRenderer>().material;
            //        newElement.GetComponent<MeshFilter>().mesh = carriedElement.GetComponent<MeshFilter>().mesh;

            //        carriedElement.SetName(null);
            //        carriedElement.GetComponent<MeshRenderer>().material = null;
            //        carriedElement.GetComponent<MeshFilter>().mesh = null;


            //        //carriedElement.GetComponent<MeshRenderer>().material = null;
            //        //carriedElement.GetComponent<MeshFilter>().mesh = null;


            //        //Instantiate(interactiveElementPrefab, carriedElement.transform);
        }
    }
    public void DropElement()
    {

    }

}