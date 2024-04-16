using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool isPlaying;

    [SerializeField]
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            // if (Input.anyKeyDown)
            //{
            //  hasStarted = true;
            //}
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
    public void ResetStartingPosition()
    {
        transform.position = startingPosition;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(i).GetComponent<NoteObject>().SetBePressed(false);
        } 
    }
}
