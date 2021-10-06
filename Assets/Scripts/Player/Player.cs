using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private int _score;

    public event UnityAction GameOver;
    public event UnityAction GameWon;
    public event UnityAction<int> ScoreChanged;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }


    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.Reset();
    }

    public void Die()
    {
        _mover.Die();
        GameOver?.Invoke();
    }

    public void Win()
    {
        _mover.Die();
        GameWon?.Invoke();
    }
}
