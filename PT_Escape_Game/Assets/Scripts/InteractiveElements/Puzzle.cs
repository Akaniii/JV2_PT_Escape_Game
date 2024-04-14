using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : StaticElement
{
    private bool isComplete;

    [SerializeField]
    protected Vector3 positionPuzzle, rotationPuzzle;

    //make a zoom and fix camera on puzzle
    protected void EnterFocusMode()
    {
        // if the player hold something while entering the puzzle, drop this element
        if (FindObjectOfType<Player>().interactionsScript.GetCarriedElement() != null)
        {
            FindObjectOfType<Player>().interactionsScript.DropCarriedElement();
        }

        //turn var of Player
        FindObjectOfType<Player>().interactionsScript.SetCurrentPuzzle(this);

        // put the player transform on right place
        FindObjectOfType<Player>().transform.position = positionPuzzle;
        FindObjectOfType<Player>().transform.eulerAngles = rotationPuzzle;

        //disable Collider
        gameObject.GetComponent<Collider>().enabled = false;

        //float elapsedTime += Time.deltaTime;
        //float pourcentageComplete = elapsedTime / 5;

        //Vector3 startPosition = FindObjectOfType<Player>().transform.position;

        //StartCoroutine(MovePlayerToPuzzle());

        //FindObjectOfType<Player>().transform.eulerAngles = Vector3.Lerp(FindObjectOfType<Player>().transform.eulerAngles, rotationPuzzle, Mathf.SmoothStep(0, 1, pourcentageComplete));

        // savedPosition in order to replace the player Camera at the right place when leaving the FocusMode
        //savedPosition = FindObjectOfType<Player>().GetCamera().transform.position;
    }

    public virtual void QuitFocusMode()
    {
        //reable Collider
        gameObject.GetComponent<Collider>().enabled = true;
        FindObjectOfType<Player>().interactionsScript.SetCurrentPuzzle(null);
    }

    public bool GetIsComplete()
    {
        return isComplete;
    }
}
