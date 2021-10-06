using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerWeaponIk))]
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private LayerMask _aimLayerMask;
    [SerializeField] private GameObject _crosshair;
    private PlayerWeaponIk _weaponIk;
    

    private void Start()
    {
        _weaponIk = GetComponent<PlayerWeaponIk>();
    }

    private void Update()
    {
        AimTowardMouse();
    }

    private void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            _weaponIk.SetTargetTransform(hitInfo.transform);
            var direction = hitInfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
        }
    }
}
