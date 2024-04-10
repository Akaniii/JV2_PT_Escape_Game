using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableElement : InteractiveElement
{
    private bool canBePicked = true;
    [SerializeField]
    private bool isKey;

    public override void Interact()
    {
        if (canBePicked)
        {
            canBePicked = false;

            transform.parent = FindObjectOfType<Player>().interactionsScript.GetHandPosition().transform;
            FindObjectOfType<Player>().interactionsScript.SetCarriedElement(this);
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

    public bool GetIsKey()
    {
        return isKey;
    }
}
