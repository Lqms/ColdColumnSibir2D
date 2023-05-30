using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _animationSpeed = 1;

    [Header("Float animation")]
    [SerializeField] private float _floatingDelta = 1;

    [Header("Color animation")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;

    private Coroutine _floatingCoroutine;
    private Coroutine _colorChangingCoroutine;

    private Color _currentFirstColor;
    private Color _currentSecondColor;
    private Vector3 _startPosition;
    private Vector3 _startScale;

    private void Start()
    {
        _startPosition = transform.position;
        _startScale = transform.localScale;

        _currentFirstColor = _firstColor;
        _currentSecondColor = _secondColor;

        _floatingCoroutine = StartCoroutine(PositionFloatAnimating());
        _colorChangingCoroutine = StartCoroutine(ColorAnimating());
    }

    private IEnumerator PositionFloatAnimating()
    {
        while (true)
        {
            transform.DOMove(transform.position + GetRandomPosition(-_floatingDelta, _floatingDelta, -_floatingDelta, _floatingDelta), 1/_animationSpeed);
            yield return new WaitForSeconds(1/_animationSpeed);
            transform.DOMove(_startPosition, 1/_animationSpeed);
            yield return new WaitForSeconds(1/_animationSpeed);
        }
    }

    private Vector3 GetRandomPosition(float minX, float maxX, float minY, float maxY)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine(_floatingCoroutine);
        StopCoroutine(_colorChangingCoroutine);
        _text.colorGradient = new VertexGradient(Color.white, Color.white, Color.white, Color.white);
        transform.localScale *= 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _floatingCoroutine = StartCoroutine(PositionFloatAnimating());
        _colorChangingCoroutine = StartCoroutine(ColorAnimating());
        transform.localScale = _startScale;
    }

    private IEnumerator ColorAnimating()
    {
        while (true)
        {
            _currentFirstColor = _firstColor;
            _currentSecondColor = _secondColor;

            while (_currentFirstColor != _secondColor && _currentSecondColor != _firstColor)
            {
                _currentFirstColor = Color.Lerp(_currentFirstColor, _secondColor, Time.deltaTime * _animationSpeed);
                _currentSecondColor = Color.Lerp(_currentSecondColor, _firstColor, Time.deltaTime * _animationSpeed);

                _text.colorGradient = new VertexGradient(_currentFirstColor, _currentSecondColor, _currentFirstColor, _currentSecondColor);

                yield return null;
            }

            var temp = _firstColor;
            _firstColor = _secondColor;
            _secondColor = temp;
        }
    }
}
