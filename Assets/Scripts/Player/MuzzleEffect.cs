using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{

    [SerializeField] GameObject _firePoint;
    [SerializeField] GameObject _vfx;

    public void MakeShootEffect()
    {
        SpawnVfx();
    }

    private void SpawnVfx()
    {
        GameObject effect;
        effect = Instantiate(_vfx, _firePoint.transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
}
