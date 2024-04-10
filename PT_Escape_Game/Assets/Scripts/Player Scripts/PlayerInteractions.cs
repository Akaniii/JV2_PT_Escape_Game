using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField]
    private GameObject handPosition;
    private MovableElement carriedElement;

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

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!CheckEnabledInteractions(hitInfo))
                    {
                        playerUIscript.ShowErrorNotification(hitInfo.collider.GetComponent<InteractiveElement>().GetErrorText());
                    }
                    else
                    {
                        hitInfo.collider.GetComponent<InteractiveElement>().Interact();
                        playerUIscript.ShowUIInteractionInput(hitInfo, false);
                    }
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
        else if (_hitInfo.collider.GetComponent<Door>() != null)
        {
            if (!_hitInfo.collider.GetComponent<Door>().GetOpened())
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        else if (_hitInfo.collider.GetComponent<Puzzle>() != null)
        {
            if  (!_hitInfo.collider.GetComponent<Puzzle>().GetIsComplete())
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

    private bool CheckEnabledInteractions(RaycastHit _hitInfo)
    {
        // if you want to pick a movable element while still holding another movable element
        if (_hitInfo.collider.GetComponent<MovableElement>() != null && carriedElement != null)
        {
            return false;
        }

        // if you want to interact with a door, but don't have the right key
        else if (_hitInfo.collider.GetComponent<Door>() != null && !_hitInfo.collider.GetComponent<Door>().CheckRightKey())
        {
            return false;
        }

        else
        {
            return true;
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

    public GameObject GetHandPosition()
    {
        return handPosition;
    }

    public MovableElement GetCarriedElement()
    {
        return carriedElement;
    }

    public void SetCarriedElement(MovableElement _carriedElement)
    {
        carriedElement = _carriedElement;
    }

    public void DestroyCarriedElement()
    {
        carriedElement.transform.position = new Vector3(0, 0, -46.51f);
        carriedElement.transform.parent = null;
        carriedElement = null;
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
