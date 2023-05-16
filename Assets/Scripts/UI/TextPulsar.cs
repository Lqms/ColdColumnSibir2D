using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPulsar : MonoBehaviour
{
    [SerializeField] private float _animationSpeed;
    [SerializeField] private Vector3 _scaleChangeValue;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Pulsaring());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Pulsaring()
    {
        while (true)
        {
            transform.localScale += _scaleChangeValue;
            yield return new WaitForSeconds(_animationSpeed * Time.deltaTime);
            transform.localScale -= _scaleChangeValue;
            yield return new WaitForSeconds(_animationSpeed * Time.deltaTime);
        }
    }
}
