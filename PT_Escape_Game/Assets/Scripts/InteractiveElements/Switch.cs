using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StaticElement
{
    [SerializeField]
    protected Light linkedLight;

    public override void Interact()
    {
        if (!linkedLight.enabled)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public virtual void TurnOn()
    {
        linkedLight.enabled = true;
        SetAction("Turn Off");
    }
    public virtual void TurnOff()
    {
        linkedLight.enabled = false;
        SetAction("Turn On");
    }

    public Light GetLight()
    {
        return linkedLight;
    }

}
