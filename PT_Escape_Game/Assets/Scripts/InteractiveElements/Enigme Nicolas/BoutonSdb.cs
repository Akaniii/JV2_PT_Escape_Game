using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonSdb : StaticElement
{

    [SerializeField] CodeToFind codeToFind;
    //private UI linkedLight;

    public override void Interact()
    {

         Pushup(codeToFind.Codex);
    }
    public int Pushup(int x)
    {
        x++;
        return x;

    }
}
