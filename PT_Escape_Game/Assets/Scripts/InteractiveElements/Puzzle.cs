using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Puzzle : StaticElement
{
    protected bool isComplete;

    [SerializeField]
    protected Vector3 positionPuzzle, rotationPuzzle;

    private Vector3 startPosition, startRotation;

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

        // put instantanly the player transform on right place
        //FindObjectOfType<Player>().transform.position = positionPuzzle;
        //FindObjectOfType<Player>().transform.eulerAngles = rotationPuzzle;

        //disable Collider
        gameObject.GetComponent<Collider>().enabled = false;

        // savedPosition in order to replace the player Camera at the right place when leaving the FocusMode
        startPosition = FindObjectOfType<Player>().transform.position;
        startRotation = FindObjectOfType<Player>().transform.eulerAngles;

        StartCoroutine(MovePlayerLerp(startPosition, startRotation, positionPuzzle, rotationPuzzle));
    }

    public IEnumerator MovePlayerLerp(Vector3 startPosition, Vector3 startRotation, Vector3 endPosition, Vector3 endRotation)
    {
        FindObjectOfType<Player>().SetCanMove(false);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            FindObjectOfType<Player>().transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / 1f);
            FindObjectOfType<Player>().transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / 1f);

            yield return null;
        }

        FindObjectOfType<Player>().SetCanMove(true);

        yield return null;
    }

    public virtual void QuitFocusMode()
    {
        //reable Collider
        gameObject.GetComponent<Collider>().enabled = true;
        FindObjectOfType<Player>().interactionsScript.SetCurrentPuzzle(null);

        StartCoroutine(MovePlayerLerp(positionPuzzle, rotationPuzzle, startPosition, startRotation));
    }

    public bool GetIsComplete()
    {
        return isComplete;
    }

    public void SetIsComplete(bool newIsComplete)
    {
        isComplete = newIsComplete;
    }
}
