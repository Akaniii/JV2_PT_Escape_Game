using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private bool CanBepressed = false;
    [SerializeField]
    private KeyCode keyToPress;

    [SerializeField]
    private bool isLastOne;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PuzzleArcade>().GetArcadeIsPlaying())
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (CanBepressed)
                {
                    gameObject.SetActive(false);
                    FindObjectOfType<PuzzleArcade>().NoteHit();

                    if (isLastOne)
                    {
                        FindObjectOfType<PuzzleArcade>().VictoryArcade();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Activator")
        {
            CanBepressed = true;
        }
        if (other.tag == "Disappear")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (isLastOne)
            {
                FindObjectOfType<PuzzleArcade>().VictoryArcade();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            CanBepressed = false;
            FindObjectOfType<PuzzleArcade>().NoteMissed();
        }

        if (other.tag == "Appear" && FindObjectOfType<PuzzleArcade>().GetArcadeIsPlaying())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void SetBePressed(bool b)
    {
        CanBepressed = b;
    }
}
