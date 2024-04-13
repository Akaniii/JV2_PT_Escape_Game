using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BathroomManager : CodesManager
{
    [SerializeField]
    private int hotnessLevel = 0;

    [SerializeField]
    private TextMeshPro[] hiddenTextsMirror;

    public int GetHotnessLevel()
    {
        return hotnessLevel;
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

    public IEnumerator MakeTextAppearLerp(TextMeshPro text)
    {
        float elapsedTime = 0f;
        Color newColor = new Color(text.color.r, text.color.g, text.color.b, 0.3f);
        Color currentColor = text.color;

        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;

            text.color = Color.Lerp(currentColor, newColor, elapsedTime/3f);
            yield return null;
        }
    }
}
