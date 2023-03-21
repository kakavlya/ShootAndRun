using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] ProjectileMover _projectileMover;
    private Vector3 _center;
    [SerializeField] private float _radius;

    private void Start()
    {
        _center = gameObject.transform.position;
    }

    private void Update()
    {
        Collider target;
        if(TryGetPlayerInRange(out target))
        {
            Debug.Log("Player detected");
            if(_projectileMover!= null)
            {
                _projectileMover.gameObject.SetActive(true);
                _projectileMover.StartMovingTowardsTarget(target);
            }            
        }
    }

    private bool TryGetPlayerInRange(out Collider target)
    {
        target = null;
        Collider[] hitColliders = Physics.OverlapSphere(_center, _radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Player player))
            {
                target = hitCollider;
                return true;
            }
        }
        return false;
    }
}
