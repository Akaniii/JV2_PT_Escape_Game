using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodesManager : MonoBehaviour
{
    [SerializeField]
    private string[] codeToFind;
    [SerializeField]
    private CodePart[] currentCode;

    public void CheckCode()
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
        Debug.Log("Right Code Found");
    }
}
