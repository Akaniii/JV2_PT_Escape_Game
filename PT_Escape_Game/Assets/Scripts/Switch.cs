using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StaticElement
{
    private bool turnedOn;

    [SerializeField]
    private Light linkedLight;

    public override void Interact()
    {
        // Animation : effectuer une rotation de la porte
        if (turnedOn)
        {
            linkedLight.enabled = false;
            turnedOn = false;
        }
        else
        {
            linkedLight.enabled = true;
            turnedOn = true;
        }
    }

}
