using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsArcade : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultImage, pressedImage;

    [SerializeField]
    private KeyCode keyToPress;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PuzzleArcade>().GetArcadeIsPlaying())
        {
            if (Input.GetKeyDown(keyToPress))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = pressedImage;
            }
            if (Input.GetKeyUp(keyToPress))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultImage;
            }
        }
    }
}
