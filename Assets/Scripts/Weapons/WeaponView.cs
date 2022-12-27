using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private WeaponData _data;

    public WeaponData Data => _data;
}
