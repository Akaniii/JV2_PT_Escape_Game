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
            finalDoor.PlaySoundEffect();
            finalDoor.SetOpened(true);
            finalDoor.GetAnimator().SetTrigger("InteractionOpen");
            music.Stop();
            FindObjectOfType<FinalPuzzle>().QuitFocusMode();
            FindObjectOfType<FinalPuzzle>().SetIsComplete(true);
        }

        else
        {
            StartCoroutine(FindObjectOfType<TimerManager>().GameOver());
        }
    }
}
