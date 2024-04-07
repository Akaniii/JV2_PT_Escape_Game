using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : StaticElement
{
    private bool opened;

    public override void Interact()
    {
        // Animation : effectuer une rotation de la porte
        if (opened)
        {
            opened = false;
        }
        else
        {
            opened = true;
        }
    }
}
