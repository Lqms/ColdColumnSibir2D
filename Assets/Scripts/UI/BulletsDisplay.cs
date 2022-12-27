using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletsDisplay : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Text _bulletsDisplay;

    private void OnEnable()
    {
        _weapon.BulletsChanged += OnBulletsChanged;
    }

    private void OnDisable()
    {
        _weapon.BulletsChanged -= OnBulletsChanged;
    }

    private void OnBulletsChanged(int currentBullets, int maxBullets)
    {
        _bulletsDisplay.text = currentBullets.ToString() + "/" + maxBullets;
    }
}
