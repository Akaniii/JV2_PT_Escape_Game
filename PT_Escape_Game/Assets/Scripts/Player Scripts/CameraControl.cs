using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity;

    private bool isCarrying;

    public Transform playerBody;

    public InteractiveElement carriedElement;

    public GameObject interactiveElementPrefab, interactiveElementsLayer;

    public LayerMask maskInteractive;
    public TextMeshProUGUI interactiveInput, dropInput;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // lock the cursor on the center on the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // track the position of arrow axis through time, and multiply it by sensitivity

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        // the rotation doesn't go too far away
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // apply rotations on the player
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Système de détection

        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 2, maskInteractive))
        {
            interactiveInput.gameObject.SetActive(true);

            interactiveInput.text = "E - Pick " + hitInfo.collider.GetComponent<InteractiveElement>().GetName();

            if (Input.GetKey(KeyCode.E) && !isCarrying)
            {
                hitInfo.collider.GetComponent<InteractiveElement>().PickObject(carriedElement);
                dropInput.gameObject.SetActive(true);
                dropInput.text = "X - Drop " + hitInfo.collider.GetComponent<InteractiveElement>().GetName();

                isCarrying = true;
            }
        }
        else
        {
            interactiveInput.gameObject.SetActive(false);
        }

        if (!isCarrying)
        {
            dropInput.gameObject.SetActive(false);
        }

        if (isCarrying && Input.GetKey(KeyCode.X))
        {
            //carriedElement.SetName(null);
            isCarrying = false;

            //carriedElement.DropObject(carriedElement, interactiveElementsLayer);

            GameObject newElement = Instantiate(interactiveElementPrefab, carriedElement.transform.position, carriedElement.transform.rotation, interactiveElementsLayer.transform);

            newElement.GetComponent<InteractiveElement>().SetName(carriedElement.GetComponent<InteractiveElement>().GetName());
            newElement.GetComponent<MeshRenderer>().material = carriedElement.GetComponent<MeshRenderer>().material;
            newElement.GetComponent<MeshFilter>().mesh = carriedElement.GetComponent<MeshFilter>().mesh;

            carriedElement.SetName(null);
            carriedElement.GetComponent<MeshRenderer>().material = null;
            carriedElement.GetComponent<MeshFilter>().mesh = null;


            //carriedElement.GetComponent<MeshRenderer>().material = null;
            //carriedElement.GetComponent<MeshFilter>().mesh = null;


            //Instantiate(interactiveElementPrefab, carriedElement.transform);
        }

    }
}
