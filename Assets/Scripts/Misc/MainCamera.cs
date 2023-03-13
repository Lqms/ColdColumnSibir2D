using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Coroutine _followingCoroutine;
    private Coroutine _lookingCoroutine;

    private const float BasePositionZ = -10;

    private void Start()
    {
        _followingCoroutine = StartCoroutine(FollowingTarget());
    }

    private void OnEnable()
    {
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
    }

    private IEnumerator FollowingTarget()
    {
        while (true)
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, BasePositionZ);
            yield return null;
        }
    }

    private IEnumerator Looking()
    {
        float sizeOfLookingArea = 0.5f;
        Vector3 topRightPoint = transform.localPosition * sizeOfLookingArea;
        Vector3 bottomLeftPoint = transform.localPosition * -sizeOfLookingArea;

        while (true)
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            transform.Translate(mouseInputX, mouseInputY, 0);

            if (transform.localPosition.y > topRightPoint.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, topRightPoint.y, BasePositionZ);
            }

            if (transform.localPosition.y < bottomLeftPoint.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, bottomLeftPoint.y, BasePositionZ);
            }

            if (transform.localPosition.x > topRightPoint.x)
            {
                transform.localPosition = new Vector3(topRightPoint.x, transform.localPosition.y, BasePositionZ);
            }

            if (transform.localPosition.x < bottomLeftPoint.x)
            {
                transform.localPosition = new Vector3(bottomLeftPoint.x, transform.localPosition.y, BasePositionZ);
            }


            yield return null;  
        }
    }

    private void OnLookKeyPressed(bool isKeyDown)
    {
        if (isKeyDown)
        {
            StopCoroutine(_followingCoroutine);
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, BasePositionZ);
            _lookingCoroutine = StartCoroutine(Looking());
        }
        else
        {
            StopCoroutine(_lookingCoroutine);
            _followingCoroutine = StartCoroutine(FollowingTarget());
        }
    }
}
