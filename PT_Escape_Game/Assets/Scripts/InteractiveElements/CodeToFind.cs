using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeToFind : MonoBehaviour
{
    [SerializeField] Door door;
    public TextMeshPro numberofcode ;
    
    //variable pour le code de SDB
    public int Codex = 0;
    public int Codey = 0;
    public int Codez = 0;
    public int Codev = 0;

    //code d'entrée
    public int Codea = 0;
    public int Codeb = 0;
    public int Codec = 0;
    public int Coded = 0;


    // Start is called before the first frame update

    public void Start()
    {
        string mescouilles = Codex.ToString();
        
        
    }

    public void CodeSdb ()
    {
        //code sdb
        if (Codex==0 && Codey==1 && Codez==3 && Codev==4)
        {
            
        }
        
        //code d'entrée
        if (Codea == 0 && Codeb == 1 && Codec == 3 && Coded == 4)
        {

        }
    }
}
