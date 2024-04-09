using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField]
    private GameObject handPosition, carriedElement;

    public LayerMask maskInteractive;

    public PlayerUI playerUIscript;

    public void InteractionsManager(Camera _mainCamera)
    {
        // if raycast centered with camera hit an interactive element (mask interactive), make UI appears and enable interaction
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out var hitInfo, 2, maskInteractive))
        {
            // if raycast detect element movable can be picked OR interactiveElement (not movables)
            if (CheckInteractionsRaycast(hitInfo))
            {
                // show input to interact
                playerUIscript.ShowUIInteractionInput(hitInfo, true);

                if ((hitInfo.collider.GetComponent<MovableElement>() != null && carriedElement == null) || hitInfo.collider.GetComponent<MovableElement>() == null)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hitInfo.collider.GetComponent<InteractiveElement>().Interact();
                        playerUIscript.ShowUIInteractionInput(hitInfo, false);
                    }
                }
                else
                {
                    playerUIscript.ShowErrorNotification();
                }
            }
            else
            {
                // hide input to interact if collide with something not Interactible
                playerUIscript.ShowUIInteractionInput(hitInfo, false);
            }
        }
        else
        {
            // hide input to interact if no collide on raycast
            playerUIscript.ShowUIInteractionInput(hitInfo, false);
        }

        DropCarriedElement();

        playerUIscript.ShowUIDropElement(carriedElement);

        RotateCarriedElement();
    }

    private bool CheckInteractionsRaycast(RaycastHit _hitInfo)
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

    private void RotateCarriedElement()
    {
        if (carriedElement != null)
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

    private void DropCarriedElement()
    {
        if (carriedElement != null && Input.GetKey(KeyCode.X))
        {
            carriedElement.GetComponent<MovableElement>().SetCanBePicked(true);
            carriedElement.GetComponent<Rigidbody>().isKinematic = false;
            carriedElement.GetComponent<Collider>().enabled = true;

            carriedElement.transform.parent = null;

            carriedElement = null;
        }
    }
}
