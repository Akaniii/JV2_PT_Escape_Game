using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

[RequireComponent (typeof (Collider))]
public abstract class InteractiveElement : MonoBehaviour
{
    [SerializeField]
    private string nameElement, action, errorText;

    [SerializeField]
    private AudioSource soundEffect;

    public void PlaySoundEffect()
    {
        soundEffect.Play();
    }

    public string GetName()
    {
        return nameElement;
    }

    public string GetAction()
    {
        return action;
    }

    public string GetErrorText()
    {
        return errorText;
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
