using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BathroomTap : StaticElement
{
    [SerializeField]
    private Animator animatorTap;

    [SerializeField] 
    private string rightOrLeft;

    [SerializeField] 
    private ParticleSystem smokeParticleSystem;

    [SerializeField] 
    private Vector3 positionParticleSystem;
    public override void Interact()
    {
        FindObjectOfType<BathroomManager>().IncreaseHotnessLevel();

        PlaySoundEffect();

        if (rightOrLeft == "left")
        {
            animatorTap.SetTrigger("RotateTap");
        }
        else if (rightOrLeft == "right")
        {
            animatorTap.SetTrigger("RotateTapRight");
        }
        gameObject.GetComponent<Collider>().enabled = false;

        // make smoke appears
        Instantiate(smokeParticleSystem, positionParticleSystem, new Quaternion(-90f, 0f, 0f, +90f));
    }
}
