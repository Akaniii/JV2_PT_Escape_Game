using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorLightManager : Switch
{
    [SerializeField]
    private SwitchColor[] lightOrder;
    
    [SerializeField]
    private SwitchColor[] switchLights;

    [SerializeField]
    private GameObject strongboxDoor, strongboxHandle;

    private int currentOrder;

    public override void Interact()
    {
        animatorSwitch.SetTrigger("PressButton");

        if (!linkedLight.activeSelf)
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
        while(!linkedLight.activeSelf)
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

                    switchLights[i].GetLightMesh().GetComponent<MeshRenderer>().material = switchLights[i].GetMaterialList()[1];
                    //enable one light
                    switchLights[i].GetLinkedLight().SetActive(true);
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
                StartCoroutine(Openstrongbox());
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

            //switchLights[i].TurnOff();
        }

        if (!linkedLight.activeSelf)
        {
            StartCoroutine("AlternationLight");
        }
    }

    public IEnumerator Openstrongbox()
    {

        strongboxHandle.GetComponent<Animator>().SetTrigger("RotateHandle");
        // rotation de la manivelle

        yield return new WaitForSeconds(1.5f);

        strongboxDoor.GetComponent<Animator>().SetTrigger("RotateDoor");
        // ouverture de la porte
    }
}
