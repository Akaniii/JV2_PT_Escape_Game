using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : Switch
{
    private bool colorLightFixed;

    public override void Interact()
    {
        // && FindObjectOfType<ColorLightManager>() => rajouter une condition qui permette de checker si on appuie sur la bonne lumière

        if (linkedLight.enabled && !colorLightFixed)
        {
            FindAnyObjectByType<ColorLightManager>().CheckRightLight(this);
            linkedLight.enabled = true;
            colorLightFixed = true;
        }
    }

    public SwitchColor GetSwitchColor()
    {
        return this;
    }

    public bool GetColorLightFixed()
    {
        return colorLightFixed;
    }

}
