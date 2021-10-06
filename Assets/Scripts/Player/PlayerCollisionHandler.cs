using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pickable pickable))
        {
            _player.AddScore();
            Destroy(other.gameObject);
        } else if(other.TryGetComponent(out FinishObj finish))
        {
            _player.Win();
        }
        else
        {
            _player.Die();
        }
    }
}
