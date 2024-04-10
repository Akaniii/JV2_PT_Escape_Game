using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorkBoardPuzzle : Puzzle
{
    [SerializeField]
    private MovableElement[] postIts;

    [SerializeField]
    private Vector3[] SlotsPostIts;

    public override void Interact()
    {
        //save the player carried element
        MovableElement playerCarriedElement = FindObjectOfType<Player>().interactionsScript.GetCarriedElement();

        // if we hold a post it
        if (playerCarriedElement != null && playerCarriedElement.GetSpecificity() == Specificity.PostIt)
        {
            // put post it on predefined slot on cork board (list of vector)

            // add post it in PostIt List

            // disable to pick it again once put on CorkBoard
            FindObjectOfType<Player>().interactionsScript.GetCarriedElement().SetCanBePicked(false);

            // out the post it from the "Hand" of the player
            FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform.parent = null;
            FindObjectOfType<Player>().interactionsScript.SetCarriedElement(null);
        }
    }
}
