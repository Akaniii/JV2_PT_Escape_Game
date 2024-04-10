using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : Switch
{
    private bool colorLightFixed;

    public override void Interact()
    {
        if (linkedLight.enabled && !colorLightFixed)
        {
            if (FindAnyObjectByType<ColorLightManager>().CheckRightLight(this))
            {
                FixOn();
            }
        }
    }

    public void FixOn()
    {
        colorLightFixed = true;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public override void TurnOff()
    {
        linkedLight.enabled = false; 
        colorLightFixed = false;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public bool GetColorLightFixed()
    {
        return colorLightFixed;
    }
    public Light GetLinkedLight()
    {
        return linkedLight;
    }

}
