using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    // bullet can fly through this also bots can see player behind this type of objects
    private const int IgnoreRaycastLayerNumber = 2;

    private void Start()
    {
        gameObject.layer = IgnoreRaycastLayerNumber;
    }
}
