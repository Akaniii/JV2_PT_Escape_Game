using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodesManager : Puzzle
{
    [SerializeField]
    private string[] codeToFind;
    [SerializeField]
    protected CodePart[] currentCode;

    public virtual void CheckCode()
    {
        int checkRightCode = 0;

        // check if each part of the current code correspond to the code to find
        for (int i = 0; i < currentCode.Length; i++)
        {
            if (currentCode[i].GetTextCodePart() == codeToFind[i])
            {
                checkRightCode++;
            }
        }

        // if all part are corrects, validate
        if (checkRightCode == currentCode.Length)
        {
            isComplete = true;

            VictoryCode();

            // disable all buttons
            for (int i = 0;i < currentCode.Length;i++)
            {
                currentCode[i].DisableButtons();
            }
        }
    }

    public virtual void VictoryCode()
    {
        // virtual method that has to be override
    }
}
