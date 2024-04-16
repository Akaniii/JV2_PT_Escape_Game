using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzle : Puzzle
{
    [SerializeField]
    private FinalButton[] finalButtons;

    [SerializeField]
    private Door door;

    public override void Interact()
    {
        if (door.GetOpened())
        {
            EnterFocusMode();
            for (int i = 0; i < finalButtons.Length; i++)
            {
                finalButtons[i].GetComponent<Collider>().enabled = true;
            }

            gameObject.GetComponent<Collider>().enabled = false;

            Collider[] colliders = gameObject.GetComponents<Collider>();
        }
    }

    public override void QuitFocusMode()
    {
        base.QuitFocusMode();

        for (int i = 0; i < finalButtons.Length; i++)
        {
            finalButtons[i].GetComponent<Collider>().enabled = false;
        }

        gameObject.GetComponent<Collider>().enabled = true;
    }

    public Door GetDoor()
    {
        return door;
    }
}
