using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorkBoardPuzzle : Puzzle
{
    [SerializeField]
    private MovableElement[] postItsList;

    [SerializeField]
    private Vector3[] SlotsPostIts;

    public override void Interact()
    {
        //save the player carried element
        MovableElement playerCarriedElement = FindObjectOfType<Player>().interactionsScript.GetCarriedElement();

        // if we hold a post it
        if (playerCarriedElement != null && playerCarriedElement.GetSpecificity() == Specificity.PostIt)
        {
            // add post it in PostIt List
            for (int i = 0; i < postItsList.Length; i++)
            {
                if (postItsList[i] == null)
                {
                    // take post it from hand of the player
                    postItsList[i] = FindObjectOfType<Player>().interactionsScript.GetCarriedElement();
                    Vector3 scalePostIt = FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform.localScale;

                    postItsList[i].transform.parent = transform;

                    // put post it on predefined slot on cork board (list of vector)
                    postItsList[i].transform.localPosition = SlotsPostIts[i];
                    postItsList[i].transform.localEulerAngles = Vector3.zero;
                    postItsList[i].transform.localScale = scalePostIt;

                    // disable collisions detection and Rigidbody for prevent the fall of post it
                    postItsList[i].GetComponent<Rigidbody>().isKinematic = true;
                    postItsList[i].GetComponent<Collider>().enabled = false;

                    // disable collisions detection and Rigidbody for prevent the fall of post it
                    postItsList[i].SetIsInPuzzle(true);
                    postItsList[i].SetCanBePicked(true);

                    break;
                }
            }

            // out the post it from the "Hand" of the player
            FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform.parent = null;
            FindObjectOfType<Player>().interactionsScript.SetCarriedElement(null);
        }
        else
        {
            EnterFocusMode();

            // reable each post It for Interactions
            for (int i = 0; i < postItsList.Length; i++)
            {
                if (postItsList[i] != null)
                {
                    postItsList[i].GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public override void QuitFocusMode()
    {
        base.QuitFocusMode();

        for (int i = 0; i < postItsList.Length; i++)
        {
            if (postItsList[i] != null)
            {
                postItsList[i].GetComponent<Collider>().enabled = false;
            }
        }
    }
}