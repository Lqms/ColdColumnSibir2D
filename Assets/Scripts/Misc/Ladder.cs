using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _changeStagePoint;
    [SerializeField] private Transform _startStagePoint;
    [SerializeField] private Ladder _mergedLadder;

    public Transform StartStagePoint => _startStagePoint;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (Vector2.Distance(_changeStagePoint.position, player.transform.position) < player.transform.localScale.x / 2)
            {
                player.transform.position = _mergedLadder.StartStagePoint.position;
            }
        }
    }
}
