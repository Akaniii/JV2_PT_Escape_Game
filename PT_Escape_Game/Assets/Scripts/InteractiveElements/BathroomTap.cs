using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BathroomTap : StaticElement
{
    [SerializeField]
    private Animator animatorTap;
    public override void Interact()
    {
        FindObjectOfType<BathroomManager>().IncreaseHotnessLevel();
        animatorTap.SetTrigger("RotateTap");
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
