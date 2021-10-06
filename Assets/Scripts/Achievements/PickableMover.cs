using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableMover : MonoBehaviour
{
    private Vector3 _center;
    [SerializeField]private float _radius;
    [SerializeField]private float _movementSpeed;

    private void Start()
    {
        _radius = 5f;
        _center = gameObject.transform.position;
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_center, _radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.TryGetComponent(out Player player))
            {
                float step = Time.deltaTime * _movementSpeed;
                var playerPosition = hitCollider.gameObject.transform;
                transform.position = Vector3.MoveTowards(transform.position, playerPosition.localPosition,step);
            }
        }
    }
}
