using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactInputInfo, dropInputInfo, errorNotification;

    public void ShowUIInteractionInput(RaycastHit _hitInfo, bool catchInteraction)
    {
        // if raycast detect element movable can be picked OR interactiveElement (not movables)
        if (catchInteraction)
        {
            // show text to let the player know what action he can make with the interactive element
            interactInputInfo.gameObject.SetActive(true);

            interactInputInfo.text = "F - " + _hitInfo.collider.GetComponent<InteractiveElement>().GetAction() + " " + _hitInfo.collider.GetComponent<InteractiveElement>().GetName();
        }
        else
        {
            interactInputInfo.gameObject.SetActive(false);
        }
    }

    public void ShowUIDropElement(MovableElement _carriedElement)
    {
        if (_carriedElement != null)
        {
            dropInputInfo.gameObject.SetActive(true);
            dropInputInfo.text = "X - Drop " + _carriedElement.GetComponent<InteractiveElement>().GetName();
        }
        else
        {
            dropInputInfo.gameObject.SetActive(false);
        }
    }

    public void ShowErrorNotification(string errorMessage)
    {
        errorNotification.gameObject.SetActive(true);
        errorNotification.text = errorMessage;

        StartCoroutine(HideErrorNotification(errorNotification));
    }

    public IEnumerator HideErrorNotification(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(false);
    }
}
