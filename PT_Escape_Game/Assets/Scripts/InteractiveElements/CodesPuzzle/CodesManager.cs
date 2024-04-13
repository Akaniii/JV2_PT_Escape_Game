using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodesManager : MonoBehaviour
{
    [SerializeField] Door door;
    public TextMeshPro numberofcode ;
    public TextMeshPro numberofcode1;

    //variable pour le code de SDB

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

    //public void Update()
    //{
    //    numberofcode.text = Codex.ToString() + Codey.ToString() + Codez.ToString()+Codez.ToString();
       
    //}
    //public void CodeSdb ()
    //{
    //    //code sdb
    //    if (Codex==0 && Codey==1 && Codez==3 && Codev==4)
    //    {
            
    //    }
        
    //    //code d'entrée
    //    if (Codea == 0 && Codeb == 1 && Codec == 3 && Coded == 4)
    //    {

    //    }
    //}
}
