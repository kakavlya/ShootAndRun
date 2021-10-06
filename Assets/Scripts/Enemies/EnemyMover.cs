using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const string IsWalkingConst = "IsWalking";
    private const string IsDeadConst = "IsDead";
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private Vector3 _startPosition;

    private Animator _animator;
    private Transform _target;
    private bool _shouldMove;
    private CapsuleCollider _collider;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shouldMove = true;
        _collider = GetComponent<CapsuleCollider>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (_target == null)
            return;
        float step = Time.deltaTime * _movementSpeed;
        if (Vector3.Distance(transform.position, _target.position) < _detectionRange && _shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.localPosition,
               step);
            var direction = -_target.position;
            direction.y = 0f;
            direction.Normalize();
            transform.LookAt(direction);

            _animator.SetFloat(IsWalkingConst, 1.0f, 9.9f, Time.deltaTime * 20f);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _animator.SetFloat("IsWalking", 0.0f);
        _animator.SetBool(IsDeadConst, false);
        _collider.enabled = true;
        _shouldMove = true;
    }

    public void Die()
    {
        _animator.SetBool(IsDeadConst, true);
        _collider.enabled = false;
        Stop();
    }

    private void Stop()
    {
        _shouldMove = false;
    }
}
