using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StaticElement
{
    [SerializeField]
    protected Light linkedLight;

    public override void Interact()
    {
        if (linkedLight.enabled)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        linkedLight.enabled = true;
        SetAction("Turn Off");
    }
    public void TurnOff()
    {
        linkedLight.enabled = false;
        SetAction("Turn On");
    }

    public Light GetLight()
    {
        return linkedLight;
    }

}
