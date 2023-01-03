using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableWeapon : InteractableObject
{
    [SerializeField] private string _message;
    [SerializeField] private Weapon _logic;

    public static event UnityAction<Weapon> PickedUp;

    public override string GetMessage()
    {
        return _message;
    }

    public override void Interact()
    {
        _logic.ResetSettings();
        PickedUp?.Invoke(_logic);
    }
}
