using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewInteractiveElement", menuName = "InteractiveElement")]

public class InteractiveScriptable : ScriptableObject
{
    public string nameElement, actionElement;
    public Mesh meshElement;
    public Material materialElement;
}
