using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool CanBepressed = false;
    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (CanBepressed)
            {
                gameObject.SetActive(false);
                ArcadeGameManager.instance.NoteHit();
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Activator")
        {
            CanBepressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            CanBepressed = false;
            ArcadeGameManager.instance.NoteMissed();
        }
    }
}
