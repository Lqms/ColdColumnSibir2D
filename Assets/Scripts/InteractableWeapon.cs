using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableWeapon : InteractableObject
{
    [SerializeField] private string _message;
    [SerializeField] private Weapon _logic;

    public static event UnityAction<InteractableWeapon> PickedUp;

    public Weapon Logic => _logic;

    public override string GetMessage()
    {
        return _message; // переписать так, чтобы автоматически формировалось сообщшение "Ќажмите  Ћј¬»ЎјƒЋя¬«ј»ћќƒ≈…—“¬»я¬«я“јя»«Ќј—“–ќ≈  дл€ подбора Ќј«¬јЌ»≈ќ–”∆»я"
    }

    public override void Interact()
    {
        PickedUp?.Invoke(this);
    }
}
