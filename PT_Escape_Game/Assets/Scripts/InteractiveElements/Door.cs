using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : StaticElement
{
    private bool opened;

    [SerializeField]
    private Animator animatorDoor;

    [SerializeField]
    private string nameRequiredKey;

    public override void Interact()
    {
        opened = true;
        animatorDoor.SetTrigger("InteractionOpen");
        FindObjectOfType<Player>().interactionsScript.DestroyCarriedElement();
    }

    public bool CheckRightKey()
    {
        MovableElement _carriedElement = FindObjectOfType<Player>().interactionsScript.GetCarriedElement();
        if (!opened && _carriedElement != null)
        {
            if (_carriedElement.GetSpecificity() == Specificity.Key && _carriedElement.GetName() == nameRequiredKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool GetOpened()
    {
        return opened;
    }
}
