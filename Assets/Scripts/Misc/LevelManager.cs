using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _enemiesLeft;

    private void Start()
    {
        _enemiesLeft = FindObjectsOfType<Enemy>().Length;
    }

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _enemiesLeft--;

        if (_enemiesLeft == 0)
        {
            print("win"); 
            // нужно запустить событие, на которое реагирует LevelInfoDisplay показывая текст о том,
            // что ты победил, аналогично сделать для проигрыша, но уже за счет события смерти игрока
            // при победе разблокируется дверь начальная и если через нее выйти, то попадаешь в триггерную область, при входе в которую пишется текст в консоль "победа"
            // потом тут будет смена уровня и т.д.
        }
    }
}
