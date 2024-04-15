using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzle : Puzzle
{
    [SerializeField]
    private FinalButton[] finalButtons;

    [SerializeField]
    private Door door;

    [SerializeField]
    private Collider [] collidersPuzzle;

    public override void Interact()
    {
        if (door.GetOpened())
        {
            EnterFocusMode();
            for (int i = 0; i < finalButtons.Length; i++)
            {
                finalButtons[i].GetComponent<Collider>().enabled = true;
            }

            for (int i = 0; i < collidersPuzzle.Length; i++)
            {
                collidersPuzzle[i].enabled = true;
            }
        }
    }

    public override void QuitFocusMode()
    {
        base.QuitFocusMode();

        for (int i = 0; i < collidersPuzzle.Length; i++)
        {
            collidersPuzzle[i].enabled = false;
        }

        for (int i = 0; i < finalButtons.Length; i++)
        {
            finalButtons[i].GetComponent<Collider>().enabled = false;
        }
    }

    public Door GetDoor()
    {
        return door;
    }
}
