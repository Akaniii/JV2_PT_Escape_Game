using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StaticElement
{
    [SerializeField]
    private Light linkedLight;

    public override void Interact()
    {
        // Animation : effectuer une rotation de la porte
        if (linkedLight.enabled)
        {
            linkedLight.enabled = false;
        }
        else
        {
            linkedLight.enabled = true;
        }
    }

}
