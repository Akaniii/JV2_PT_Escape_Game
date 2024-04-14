using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodePart : MonoBehaviour
{
    [SerializeField]
    private CodesManager codeManager;

    [SerializeField]
    private int currentStep;

    [SerializeField]
    private string[] listPossibilities;

    [SerializeField]
    private TextMeshPro textCodePart;

    public void ChangeCurrentStep(int changement)
    {
        currentStep += changement;
    }
    public void ResetCurrentStep(int newIndex)
    {
        currentStep = newIndex;
    }

    public int GetCurrentStep()
    {
        return currentStep;
    }

    public string[] GetListPossibilities()
    {
        return listPossibilities;
    }

    public string GetTextCodePart()
    {
        return textCodePart.text;
    }

    public void SetTextCodePart(string newText)
    {
        textCodePart.text = newText;
        codeManager.CheckCode();
    }

    public void DisableButtons()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
