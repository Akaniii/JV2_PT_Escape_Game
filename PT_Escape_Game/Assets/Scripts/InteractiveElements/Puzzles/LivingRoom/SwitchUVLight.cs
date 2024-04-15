using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchUVLight : Switch
{
    public override void Interact()
    {
        base.Interact();

        FindObjectOfType<ColorLightManager>().ManageUVTextPostIts();
    }
}
