using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class InteractiveElement : MonoBehaviour
{
    [SerializeField]
    private string nameElement, action;
    
    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshRenderer meshRenderer;

    //public GameObject interactiveElementPrefab;

    [SerializeField]
    //private bool isCarriedElement;

    // Start is called before the first frame update
    void Start()
    {
        //if (!isCarriedElement)
        //{
        //    gameObject.GetComponent<InteractiveElement>().SetAction(infosElement.actionElement);
        //    gameObject.GetComponent<InteractiveElement>().SetName(infosElement.nameElement);
        //    gameObject.GetComponent<MeshFilter>().mesh = infosElement.meshElement;
        //    gameObject.GetComponent<MeshRenderer>().material = infosElement.materialElement;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public string GetName()
    {
        return nameElement;
    }

    public string GetAction()
    {
        return action;
    }

    public void SetName(string newName)
    {
        nameElement = newName;
    }

    public void SetAction(string newAction)
    {
        action = newAction;
    }

    public virtual void Interact()
    {

    }

    public void PickObject(InteractiveElement carriedObject)
    {
        carriedObject.SetName(nameElement);
        carriedObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
        carriedObject.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshFilter>().mesh;

        Destroy(gameObject);

    }
}
