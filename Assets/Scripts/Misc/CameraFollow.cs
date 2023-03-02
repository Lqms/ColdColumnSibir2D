using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Crosshair _crosshair;

    private Coroutine _coroutine;
    private const float BasePositionZ = -10;

    private void Start()
    {
        _coroutine = StartCoroutine(FollowingTarget(_player.transform));
    }

    private void OnEnable()
    {
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
    }

    private IEnumerator FollowingTarget(Transform target)
    {
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            transform.position = new Vector3(target.position.x, target.position.y, BasePositionZ);
            yield return null;
        }
    }

    private void OnLookKeyPressed(bool isDown)
    {
        var target = isDown ? _crosshair.transform : _player.transform;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FollowingTarget(target));
    }
}
