using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private InteractableObjectHintDisplay _hintDisplay;

    private List<InteractableObject> _interactableObjects = new List<InteractableObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractableObject interactableObject))
        {
            _interactableObjects.Add(interactableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractableObject interactableObject))
        {
            if (_interactableObjects.Contains(interactableObject))
                _interactableObjects.Remove(interactableObject);
        }
    }

    private void Update()
    {
        if (_interactableObjects.Count > 0)
        {
            var closestObject = DetermineClosestObject();
            _hintDisplay.Activate(closestObject);
        }
        else
        {
            _hintDisplay.Deactivate();
        }
    }

    public InteractableObject DetermineClosestObject()
    {
        float distance = float.MaxValue;
        InteractableObject closestObject = null;

        foreach (var interactableObject in _interactableObjects)
        {
            if (Vector2.Distance(transform.position, interactableObject.transform.position) < distance)
            {
                closestObject = interactableObject;
                distance = Vector2.Distance(transform.position, interactableObject.transform.position);
            }
        }

        return closestObject;
    }
}
