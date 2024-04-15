using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeGameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startPlaying=false;
    public BeatScroller theBS;
    public static ArcadeGameManager instance;

    //Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                startPlaying = true;
                theBS.hasStarted = true;

                music.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("1");

    }
    public void NoteMissed()
    {
        Debug.Log("0");
    }
}
