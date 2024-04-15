using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButton : StaticElement
{
    [SerializeField]
    private bool correctButton;

    [SerializeField]
    private Door finalDoor;

    [SerializeField]
    private AudioSource music;

    public override void Interact()
    {
        PlaySoundEffect();

        if (correctButton)
        {
            finalDoor.Interact();
            music.Stop();
        }

        else
        {
            StartCoroutine(FindObjectOfType<TimerManager>().GameOver());
        }
    }
}
