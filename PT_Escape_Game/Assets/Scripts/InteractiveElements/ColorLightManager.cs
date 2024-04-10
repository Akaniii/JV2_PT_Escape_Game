using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightManager : MonoBehaviour
{
    [SerializeField]
    private SwitchColor[] lightOrder;
    
    [SerializeField]
    private SwitchColor[] switchLights;

    private int currentOrder;

    [SerializeField]
    private Switch triggerLight;

    private void Start()
    {
        ColorLightSystem();
    }

    /*public void LaunchColorLightSystem()
    {
        if (!triggerLight.GetLight().enabled)
        {
            ColorLightSystem();
        }
    }*/

    public void ColorLightSystem()
    {
        StartCoroutine(AlternationLight());
    }

    public IEnumerator AlternationLight()
    {
        float timerLightSystem = 1.5f;
        // loop the system
        for (int t = 0; t < 8; t++)
        {
            //enable light
            for (int i = 0; i < switchLights.Length; i++)
            {
                yield return new WaitForSeconds(timerLightSystem);

                // loop to disable all lights
                for (int j = 0; j < switchLights.Length; j++)
                {
                    // if light hasn't been fixed by player action
                    if (!switchLights[j].GetColorLightFixed())
                    {
                        switchLights[j].TurnOff();
                    }
                }

                //enable one light
                switchLights[i].TurnOn();
            }

            timerLightSystem -= .15f;
        }

        DisableAllLights();
    }

    public void CheckRightLight(SwitchColor switchPressed)
    {
        if (lightOrder[currentOrder] == switchPressed)
        {
            currentOrder++;

            //victoryCheck
            if (currentOrder == 4)
            {
                // Fonction de victoire
            }
        }
        else
        {
            DisableAllLights();
            currentOrder = 0;
        }
    }

    public void DisableAllLights()
    {
        // disable all lights
        for (int i = 0; i < switchLights.Length; i++)
        {
            switchLights[i].TurnOff();
        }
    }

    public SwitchColor[] GetLightOrder()
    {
        return lightOrder;
    }

    public SwitchColor[] GetSwitchLightsList()
    {
        return switchLights;
    }
}
