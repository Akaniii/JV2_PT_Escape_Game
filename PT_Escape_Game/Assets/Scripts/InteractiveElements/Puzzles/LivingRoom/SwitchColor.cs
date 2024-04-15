using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : Switch
{
    private bool colorLightFixed;

    [SerializeField]
    protected GameObject tinyLight;

    [SerializeField]
    protected GameObject tinyLightMesh;

    public override void Interact()
    {
        if (linkedLight.activeSelf && !colorLightFixed)
        {
            animatorSwitch.SetTrigger("PressButton");
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

        tinyLight.SetActive(true);
        tinyLightMesh.GetComponent<MeshRenderer>().material = materialLight[1];
        animatorSwitch.SetTrigger("PressButton");
    }

    public override void TurnOff()
    {
        linkedLight.SetActive(false); 
        colorLightFixed = false;
        lightMesh.GetComponent<MeshRenderer>().material = materialLight[0];
        gameObject.GetComponent<Collider>().enabled = true;

        tinyLight.SetActive(false);
        tinyLightMesh.GetComponent<MeshRenderer>().material = materialLight[0];
    }

    public bool GetColorLightFixed()
    {
        return colorLightFixed;
    }
    public GameObject GetLinkedLight()
    {
        return linkedLight;
    }

    public GameObject GetLightMesh()
    {
        return lightMesh;
    }

    public Material[] GetMaterialList()
    {
        return materialLight;
    } 

}
