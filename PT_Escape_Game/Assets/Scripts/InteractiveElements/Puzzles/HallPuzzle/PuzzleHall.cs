using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleHall : CodesManager
{
    [SerializeField]
    private GameObject boxDoor, reward;

    public override void Interact()
    {
        // if we hold a post it
        if (!isComplete)
        {
            EnterFocusMode();
            for (int i = 0; i < currentCode.Length; i++)
            {
                currentCode[i].RenableButtons();
            }
        }
    }

    public override void QuitFocusMode()
    {
        base.QuitFocusMode();

        for (int i = 0; i < currentCode.Length; i++)
        {
            currentCode[i].DisableButtons();
        }
    }

    public override void VictoryCode()
    {
        isComplete = true;
        QuitFocusMode();

        boxDoor.GetComponent<Animator>().SetTrigger("OpenTop");
        reward.GetComponent<MovableElement>().SetCanBePicked(true);
    }
}
