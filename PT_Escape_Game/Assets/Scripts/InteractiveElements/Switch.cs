using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StaticElement
{
    [SerializeField]
    protected GameObject linkedLight;

    [SerializeField]
    protected GameObject lightMesh;

    [SerializeField]
    protected Material [] materialLight;

    [SerializeField]
    protected Animator animatorSwitch;

    public override void Interact()
    {
        animatorSwitch.SetTrigger("PressButton");
        PlaySoundEffect();

        if (!linkedLight.activeSelf)
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
        linkedLight.SetActive(true);
        lightMesh.GetComponent<MeshRenderer>().material = materialLight[1];
        SetAction("Turn Off");
    }
    public virtual void TurnOff()
    {
        linkedLight.SetActive(false);
        lightMesh.GetComponent<MeshRenderer>().material = materialLight[0];
        SetAction("Turn On");
    }

    public GameObject GetLight()
    {
        return linkedLight;
    }

}
