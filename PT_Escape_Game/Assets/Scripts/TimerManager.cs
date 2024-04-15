using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private float timerGame, maxTimer;

    [SerializeField] 
    private bool finalVictory;

    [SerializeField]
    private Light [] allLightsList, C4lightsList;

    [SerializeField]
    private AnimationCurve curveLight, curveBombBlinkLights;

    [SerializeField]
    private Color newColor;

    [SerializeField]
    private Color[] previousColor;

    private void Start()
    {
        for (int i = 0; i < allLightsList.Length; i++)
        {
            previousColor[i] = allLightsList[i].color;
        }

        StartCoroutine(TimerElapsesMethod());
    }

    private void Update()
    {
        if (!finalVictory && timerGame < maxTimer)
        {
            timerGame += Time.deltaTime;

            for (int i = 0; i < allLightsList.Length; i++)
            {
                allLightsList[i].color = Color.Lerp(previousColor[i], newColor, curveLight.Evaluate(timerGame/maxTimer));
            }
        }
    }

    public IEnumerator TimerElapsesMethod()
    {
        while (!finalVictory && timerGame < maxTimer)
        {
            yield return new WaitForSeconds(curveBombBlinkLights.Evaluate(timerGame / maxTimer));
            
            // disable the lights
            for (int i = 0; i < C4lightsList.Length; i++)
            {
                C4lightsList[i].enabled = false;
            }

            yield return new WaitForSeconds(curveBombBlinkLights.Evaluate(timerGame / maxTimer));

            // reable the lights
            for (int i = 0; i < C4lightsList.Length; i++)
            {
                C4lightsList[i].enabled = true;
            }
        }
    }
}
