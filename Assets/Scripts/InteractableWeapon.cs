using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWeapon : InteractableObject
{
    [SerializeField] private string _message;

    public override string GetMessage()
    {
        return _message;
    }

    public override void Interact()
    {
        print("picked up weapon");
    }
}
