using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfHearing : MonoBehaviour
{
    public event UnityAction TargetDetected;

    private void Start()
    {
        print("При выстреле игрок создает звук на точке выстрела и у него есть радиус распространения, проверять" +
            "с помощью overlapcircleall все объекты попавшие в эту зону и проверять наличие слуха у них" +
            "и вызывать метод что тип они игрока усылашил а в нем событие запускать");
    }
}
