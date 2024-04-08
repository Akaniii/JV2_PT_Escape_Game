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

            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            FindObjectOfType<Player>().SetCarriedElement(gameObject);
            FindObjectOfType<Player>().SetHoldSomething(true);

            Destroy(gameObject);
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
