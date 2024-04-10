using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLightManager : Switch
{
    [SerializeField]
    private SwitchColor[] lightOrder;
    
    [SerializeField]
    private SwitchColor[] switchLights;

    private int currentOrder;

    public override void Interact()
    {
        if (!linkedLight.enabled)
        {
            // turn on normal light
            TurnOn();

            if (currentOrder < 4)
            {
                // turn off all colored lights
                DisableAllLights();
            }
        }
        else
        {
            // turn off normal light
            TurnOff();

            // start alterned turn on of colored lights (must be named with string to stop it)
            StartCoroutine("AlternationLight");
        }
    }

    public IEnumerator AlternationLight()
    {
        while(!linkedLight.enabled)
        {
            yield return new WaitForSeconds(2);
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
                    switchLights[i].GetLinkedLight().enabled = true;
                }

                timerLightSystem -= .15f;
            }

            DisableAllLights();
        }
    }

    public bool CheckRightLight(SwitchColor switchPressed)
    {
        if (lightOrder[currentOrder] == switchPressed)
        {
            currentOrder++;

            //victoryCheck
            if (currentOrder == 4)
            {
                // Fonction de victoire
            }

            return true;
        }
        else
        {
            DisableAllLights();
            return false;
        }
    }

    public void DisableAllLights()
    {
        currentOrder = 0;
        StopCoroutine("AlternationLight");

        // disable all lights
        for (int i = 0; i < switchLights.Length; i++)
        {
            switchLights[i].TurnOff();
        }

        if (!linkedLight.enabled)
        {
            StartCoroutine("AlternationLight");
        }
    }
}
