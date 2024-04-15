using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;

public class BathroomManager : CodesManager
{
    [SerializeField]
    private int hotnessLevel = 0;

    [SerializeField] 
    private int stepIncrease = 0, maxSteps;

    [SerializeField]
    private TextMeshPro[] hiddenTextsMirror;

    [SerializeField]
    private GameObject[] lightsMeshes, lights;

    [SerializeField]
    private Material[] lightMaterials;

    [SerializeField]
    private GameObject boxDoor, reward;

    [SerializeField]
    private AudioSource wrongBuzzer;

    private bool isReseting;

    public override void Interact()
    {
        // if we hold a post it
        if (!isComplete && !isReseting)
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

    public int GetHotnessLevel()
    {
        return hotnessLevel;
    }

    public bool GetIsReseting()
    {
        return isReseting;
    }

    public void IncreaseHotnessLevel()
    {
        hotnessLevel++;

        if (hotnessLevel == 3)
        {
            for (int i = 0; i < hiddenTextsMirror.Length; i++)
            {
                StartCoroutine(MakeTextAppearLerp(hiddenTextsMirror[i]));
            }
        }
    }

    public override void CheckCode()
    {
        base.CheckCode();

        // the player has limited tries before it reboot
        AddStepIncrease();
    }

    public void AddStepIncrease()
    {
        if (stepIncrease < 5)
        {
            lights[stepIncrease].SetActive(true);
            lightsMeshes[stepIncrease].GetComponent<MeshRenderer>().material = lightMaterials[1];
        }

        stepIncrease++;

        if (stepIncrease == maxSteps && !isComplete)
        {
            stepIncrease = 0;

            StartCoroutine(ErrorCode());
        }
    }

    public override void VictoryCode()
    {
        PlaySoundEffect();

        isComplete = true;
        QuitFocusMode();

        //gameObject.GetComponent<Collider>().enabled = false;

        boxDoor.GetComponent<Animator>().SetTrigger("OpenTop");
        reward.GetComponent<MovableElement>().SetCanBePicked(true);
    }

    public IEnumerator MakeTextAppearLerp(TextMeshPro text)
    {
        float elapsedTime = 0f;
        Color newColor = new Color(text.color.r, text.color.g, text.color.b, 0.3f);
        Color currentColor = text.color;

        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;

            text.color = Color.Lerp(currentColor, newColor, elapsedTime/5f);
            yield return null;
        }
    }

    public IEnumerator ErrorCode()
    {
        isReseting = true;

        wrongBuzzer.Play();

        for (int i = 0; i < currentCode.Length; i++)
        {
            currentCode[i].DisableButtons();
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                lights[j].SetActive(false);
                lightsMeshes[j].GetComponent<MeshRenderer>().material = lightMaterials[0];
            }

            yield return new WaitForSeconds(.5f);

            for (int k = 0; k < 5; k++)
            {
                lights[k].SetActive(true);
                lightsMeshes[k].GetComponent<MeshRenderer>().material = lightMaterials[1];
            }

            yield return new WaitForSeconds(.5f);
        }

        for (int j = 0; j < 5; j++)
        {
            lights[j].SetActive(false);
            lightsMeshes[j].GetComponent<MeshRenderer>().material = lightMaterials[0];
        }

        for (int i = 0; i < 6; i++)
        {
            CodePart code = transform.GetChild(i).gameObject.GetComponent<CodePart>();

            // reset the original text & step
            transform.GetChild(i).gameObject.GetComponent<CodePart>().SetTextCodePart(code.GetListPossibilities()[code.GetOriginalStep()]);
            transform.GetChild(i).gameObject.GetComponent<CodePart>().ResetCurrentStep(code.GetOriginalStep());
        }

        if (FindObjectOfType<Player>().interactionsScript.GetCurrentPuzzle() != null)
        {
            if ((FindObjectOfType<Player>().interactionsScript.GetCurrentPuzzle().gameObject.GetComponent<BathroomManager>() != null))
            {
                for (int i = 0; i < currentCode.Length; i++)
                {
                    currentCode[i].RenableButtons();
                }
            }
        }

        isReseting = false;
    }
}
