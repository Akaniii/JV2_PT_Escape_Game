using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private float timerGame, maxTimer;

    [SerializeField] 
    private bool finalVictory, gameOver;

    [SerializeField]
    private Light [] allLightsList, C4lightsList;

    [SerializeField]
    private AnimationCurve curveLight, curveBombBlinkLights;

    [SerializeField]
    private Color newColor;

    [SerializeField]
    private Color[] previousColor;

    [SerializeField]
    private AudioSource bombSound, bipSound, music;

    [SerializeField]
    private Image blackScreen;

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

        else if (timerGame >= maxTimer && !gameOver && !finalVictory)
        {
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        gameOver = true;
        FindObjectOfType<Player>().SetCanMove(false);
        blackScreen.GetComponent<Animator>().SetTrigger("GameOver");
        music.Stop();
        yield return new WaitForSeconds(1f);
        bombSound.Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            bipSound.Play();

            // reable the lights
            for (int i = 0; i < C4lightsList.Length; i++)
            {
                C4lightsList[i].enabled = true;
            }
        }
    }
}
