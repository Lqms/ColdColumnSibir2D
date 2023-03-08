using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Car : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;

    private BoxCollider2D _collider;

    private void OnEnable()
    {
        _levelManager.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _levelManager.GameOver -= OnGameOver;
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            print("yohooo win");
    }

    private void OnGameOver(LevelStates state)
    {
        _collider.isTrigger = true;
    }
}
