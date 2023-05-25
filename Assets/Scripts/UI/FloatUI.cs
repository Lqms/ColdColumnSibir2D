using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUI : MonoBehaviour
{
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private float _floatingDelta = 2;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(Floating());
    }

    private IEnumerator Floating()
    {
        while (true)
        {
            Vector3 randomPosition = GetRandomPosition();

            while (Vector3.Distance(transform.position, randomPosition) > 1)
            {
                print("to random pos");
                transform.position = Vector3.MoveTowards(transform.position, randomPosition, _floatingDelta * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime / _animationSpeed);
            }

            while (Vector3.Distance(transform.position, _startPosition) > 1)
            {
                print("to start pos");
                transform.position = Vector3.MoveTowards(transform.position, _startPosition, _floatingDelta * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime / _animationSpeed);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        return new Vector3(randomX, randomY);
    }
}
