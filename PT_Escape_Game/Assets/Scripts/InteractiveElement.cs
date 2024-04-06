using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    public InteractiveScriptable infosElement;

    private string nameElement;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public GameObject interactiveElementPrefab;

    [SerializeField]
    private bool isCarriedElement;

    // Start is called before the first frame update
    void Start()
    {
        if (!isCarriedElement)
        {
            gameObject.GetComponent<InteractiveElement>().SetName(infosElement.nameElement);
            gameObject.GetComponent<MeshFilter>().mesh = infosElement.meshElement;
            gameObject.GetComponent<MeshRenderer>().material = infosElement.materialElement;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public string GetName()
    {
        return nameElement;
    }

    public void SetName(string newName)
    {
        nameElement = newName;
    }

    public void PickObject(InteractiveElement carriedObject)
    {
        carriedObject.SetName(nameElement);
        carriedObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
        carriedObject.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshFilter>().mesh;

        Destroy(gameObject);

    }
}
