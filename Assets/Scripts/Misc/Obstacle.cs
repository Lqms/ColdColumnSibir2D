using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // bullet can't fly through this also bots can't see player behind this type of objects
    private const int ObstacleLayerNumber = 6;

    private void Start()
    {
        gameObject.layer = ObstacleLayerNumber;
    }
}
