using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObjectHintDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _text;
   
    public void Activate(InteractableObject interactableObject)
    {
        transform.position = interactableObject.transform.position;
        _text.text = interactableObject.GetMessage();
        _canvas.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _canvas.gameObject.SetActive(false);
    }
}
