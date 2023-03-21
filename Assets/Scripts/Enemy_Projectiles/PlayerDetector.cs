using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private Vector3 _center;
    [SerializeField] private float _radius;

    private void Start()
    {
        _center = gameObject.transform.position;
    }

    public bool TryGetPlayerInRange(out Collider target)
    {
        target = null;
        Collider[] hitColliders = Physics.OverlapSphere(_center, _radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Player player))
            {
                target =  hitCollider;
                return true;
            }
        }
        return false;
    }
}
