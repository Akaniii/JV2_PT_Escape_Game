using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : StaticElement
{
    [SerializeField]
    private TypeAction typeAction;

    [SerializeField]
    private CodePart codePart;

    public override void Interact()
    {
        int currentCode = codePart.GetCurrentStep();
        string [] listPossibilies = codePart.GetListPossibilities();

        if (typeAction == TypeAction.Increase)
        {
            // if currentCode is at the max, reset to 0
            if (currentCode == listPossibilies.Length - 1)
            {
                codePart.ResetCurrentStep(0);
            }

            // add 1
            else
            {
                codePart.ChangeCurrentStep(1);
            }
        }
        else
        {
            // if currentCode is at 0 index, reset to max
            if (currentCode == 0)
            {
                codePart.ResetCurrentStep(listPossibilies.Length - 1);
            }

            // substract 1
            else
            {
                codePart.ChangeCurrentStep(-1);
            }
        }

        //apply modifications
        codePart.SetTextCodePart(listPossibilies[codePart.GetCurrentStep()]);
        codePart.GetCodesManager().CheckCode();
    }
}

public enum TypeAction
{
    Increase,
    Decrease
}
