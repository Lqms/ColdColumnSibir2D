using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfHearing : MonoBehaviour
{
    public event UnityAction TargetDetected;

    private void Start()
    {
        print("field of hearing");
    }
}
