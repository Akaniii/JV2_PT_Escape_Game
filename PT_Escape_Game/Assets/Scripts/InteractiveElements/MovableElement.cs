using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableElement : InteractiveElement
{
    private bool canBePicked = true;

    public override void Interact()
    {
        if (canBePicked)
        {
            canBePicked = false;

            transform.parent = FindObjectOfType<Player>().interactionsScript.GetCarriedElement().transform;
            FindObjectOfType<Player>().interactionsScript.SetCarriedElement(gameObject);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero; 

            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public bool GetCanBePicked()
    {
        return canBePicked;
    }

    public void SetCanBePicked(bool newBool)
    {
        canBePicked = newBool;
    }
}
