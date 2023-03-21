using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private bool _shouldMove;
    private Collider _target;

    public void StartMovingTowardsTarget(Collider target)
    {
        _shouldMove = true;
        _target = target;
    }

    private void Update()
    {
        if (_shouldMove)
            MoveTowardsTarget(_target);
    }

    private void MoveTowardsTarget(Collider target)
    {
        float step = Time.deltaTime * _movementSpeed;
        var playerPosition = target.gameObject.transform;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.localPosition, step);
    }
}
