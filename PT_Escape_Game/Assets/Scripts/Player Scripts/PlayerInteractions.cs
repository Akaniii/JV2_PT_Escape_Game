using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField]
    private GameObject handPosition;
    private MovableElement carriedElement;
    private Puzzle currentPuzzle;

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

        BackInteraction();

        playerUIscript.ShowUIBack(carriedElement);

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

        else if (_hitInfo.collider.GetComponent<BathroomManager>() != null)
        {
            if (!_hitInfo.collider.GetComponent<BathroomManager>().GetIsReseting() && !_hitInfo.collider.GetComponent<BathroomManager>().GetIsComplete())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else if (_hitInfo.collider.GetComponent<FinalPuzzle>() != null)
        {
            if (_hitInfo.collider.GetComponent<FinalPuzzle>().GetDoor().GetOpened() && !_hitInfo.collider.GetComponent<FinalPuzzle>().GetIsComplete())
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
            if (!_hitInfo.collider.GetComponent<Puzzle>().GetIsComplete() && currentPuzzle == null)
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
        if (_hitInfo.collider.GetComponent<MovableElement>() != null && carriedElement != null && currentPuzzle == null)
        {
            return false;
        }

        // if you want to interact with a door, but don't have the right key
        else if (_hitInfo.collider.GetComponent<Door>() != null && !_hitInfo.collider.GetComponent<Door>().CheckRightKey())
        {
            return false;
        }

        else if (_hitInfo.collider.GetComponent<PuzzleArcade>() != null && !_hitInfo.collider.GetComponent<PuzzleArcade>().GetIsPowered())
        {
            if (carriedElement != null && carriedElement.GetName() == "Cable")
            {
                return true;
            }
            else { return false; }
        }

        // if you want to interact with a switch color, but the light is off
        else if (_hitInfo.collider.GetComponent<SwitchColor>() != null && !_hitInfo.collider.GetComponent<SwitchColor>().GetLinkedLight().activeSelf)
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
                carriedElement.transform.Rotate(0f, -3f, 0f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                carriedElement.transform.Rotate(0f, 3f, 0f);
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

    public Puzzle GetCurrentPuzzle()
    {
        return currentPuzzle;
    }

    public void SetCurrentPuzzle(Puzzle newPuzzle)
    {
        currentPuzzle = newPuzzle;
    }

    public void DestroyCarriedElement()
    {
        carriedElement.transform.position = new Vector3(0, 0, -46.51f);
        carriedElement.transform.parent = null;
        carriedElement = null;
    }

    private void BackInteraction()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentPuzzle != null)
            {
                if (carriedElement != null)
                {
                    carriedElement.transform.localPosition -= new Vector3(0.2f, 0f, 0f);

                    carriedElement.GetComponent<MovableElement>().SetCanBePicked(true);
                    carriedElement.GetComponent<Collider>().enabled = true;
                    carriedElement = null;
                }
                else
                {
                    //go out of the puzzle
                    currentPuzzle.QuitFocusMode();
                }
            }
            else if (carriedElement != null)
            {
                DropCarriedElement();
            }
        }
    }

    public void DropCarriedElement()
    {
        carriedElement.GetComponent<MovableElement>().SetCanBePicked(true);
        carriedElement.GetComponent<Rigidbody>().isKinematic = false;
        carriedElement.GetComponent<Collider>().enabled = true;

        carriedElement.transform.parent = null;
        carriedElement = null;
    }
}