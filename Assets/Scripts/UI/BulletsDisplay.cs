using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsDisplay : MonoBehaviour
{
    [SerializeField] private Text _bulletsDisplay;

    private Weapon _weapon;

    private void OnEnable()
    {
        InteractableWeapon.PickedUp += OnWeaponPickedUp;
    }

    private void OnDisable()
    {
        InteractableWeapon.PickedUp -= OnWeaponPickedUp;
    }

    private void OnBulletsChanged(int currentBullets)
    {
        _bulletsDisplay.text = currentBullets.ToString() + "rnds";
    }

    private void OnWeaponPickedUp(InteractableWeapon weapon)
    {
        if (_weapon != null)
            _weapon.BulletsChanged -= OnBulletsChanged;

        _weapon = weapon.Logic;

        _weapon.BulletsChanged += OnBulletsChanged;
    }
}
