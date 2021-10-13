using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class DestroyableObstacle : ShootableObject
{

    [SerializeField] ParticleSystem _shootEffect;
    [SerializeField] private float _dieDuration;
    private void Awake()
    {
        _shootEffect.Stop(true);   
    }
    public override void GetHit()
    {
        _shootEffect.Play();
        StartCoroutine(DieAfterDuration(1.5f));
    }
}
