using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleArcade : Puzzle
{
    [SerializeField]
    private GameObject boxDoor, reward, cable;

    [SerializeField]
    private AudioSource musicArcade, music, wrongBuzzer;

    [SerializeField]
    private BeatScroller theBS;

    [SerializeField]
    private int missedNote, maxMissedNote;

    private bool isPowered, arcadeIsPlaying;
    
    [SerializeField]
    private ButtonsArcade[] buttonsArcade;

    [SerializeField]
    private TextMeshPro text;

    public override void Interact()
    {
        //save the player carried element
        MovableElement playerCarriedElement = FindObjectOfType<Player>().interactionsScript.GetCarriedElement();

        // if we hold a post it
        if (playerCarriedElement != null && playerCarriedElement.GetName() == "Cable")
        {
            FindObjectOfType<Player>().interactionsScript.DestroyCarriedElement();
            cable.gameObject.SetActive(true);

            isPowered = true;

            for (int i = 0; i < buttonsArcade.Length; i++)
            {
                buttonsArcade[i].gameObject.SetActive(true);
            }
            text.gameObject.SetActive(true);

            PlaySoundEffect();
        }
        else if (isPowered && !isComplete) 
        {
            EnterFocusMode();

            arcadeIsPlaying = true;
            theBS.isPlaying = true;
            musicArcade.Play();
            music.Pause();
        }
    }
    public override void QuitFocusMode()
    {
        base.QuitFocusMode();
        arcadeIsPlaying = false;
        theBS.isPlaying = false;
       
        musicArcade.Stop();
        music.Play();

        missedNote = 0;

        theBS.ResetStartingPosition();
    }

    public bool GetIsPowered()
    {
        return isPowered;
    }

    public bool GetArcadeIsPlaying()
    {
        return arcadeIsPlaying;
    }

    public void VictoryArcade()
    {
        isComplete = true;
        boxDoor.GetComponent<Animator>().SetTrigger("OpenTop");
        reward.SetActive(true);

        QuitFocusMode();
    }

    public void NoteHit()
    {

    }
    public void NoteMissed()
    {
        missedNote++;

        if (missedNote > maxMissedNote)
        {
            wrongBuzzer.Play();
            //Game Over Arcade Puzzle
            QuitFocusMode();
        }
    }
}
