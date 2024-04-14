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

    private int originalStep;

    [SerializeField]
    private string[] listPossibilities;

    [SerializeField]
    private TextMeshPro textCodePart;

    public void Start()
    {
        originalStep = currentStep;
    }

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
    public int GetOriginalStep()
    {
        return originalStep;
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
    }

    public CodesManager GetCodesManager()
    {
        return codeManager;
    }

    public void DisableButtons()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
