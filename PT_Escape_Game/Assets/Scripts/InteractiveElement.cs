using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent (typeof (Collider))]
public abstract class InteractiveElement : MonoBehaviour
{
    [SerializeField]
    private string nameElement, action;
   
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
}
