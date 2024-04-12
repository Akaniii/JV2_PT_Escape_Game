using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableElement : InteractiveElement
{
    [SerializeField]
    protected bool canBePicked = true;
    [SerializeField]
    protected Specificity specificity;

    private bool isInPuzzle;

    public override void Interact()
    {
        if (canBePicked && !isInPuzzle)
        {
            canBePicked = false;

            transform.parent = FindObjectOfType<Player>().interactionsScript.GetHandPosition().transform;
            FindObjectOfType<Player>().interactionsScript.SetCarriedElement(this);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero; 

            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else if (isInPuzzle)
        {
            if (FindObjectOfType<Player>().interactionsScript.GetCarriedElement())
            {
                // switch the position of each post it
                Vector3 pastPosition = transform.localPosition;

                transform.localPosition = FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform.localPosition;
                transform.localPosition -= new Vector3(0.2f, 0f, 0f);

                FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform.localPosition = pastPosition;

                FindObjectOfType<Player>().interactionsScript.GetCarriedElement().GetComponent<Collider>().enabled = true;
                FindObjectOfType<Player>().interactionsScript.GetCarriedElement().SetCanBePicked(true);
                FindObjectOfType<Player>().interactionsScript.SetCarriedElement(null);
            }

            else
            {
                // move forward the post it
                transform.localPosition += new Vector3(0.2f, 0f, 0f);
                
                // set the carried element on Player
                FindObjectOfType<Player>().interactionsScript.SetCarriedElement(this);
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public bool GetIsInPuzzle()
    {
        return isInPuzzle;
    }

    public void SetIsInPuzzle(bool newBool)
    {
        isInPuzzle = newBool;
    }

    public bool GetCanBePicked()
    {
        return canBePicked;
    }

    public void SetCanBePicked(bool newBool)
    {
        canBePicked = newBool;
    }

    public Specificity GetSpecificity()
    {
        return specificity;
    }
}

public enum Specificity
{
    Nothing,
    Key,
    PostIt
}


