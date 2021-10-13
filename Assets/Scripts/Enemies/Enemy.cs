using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))] 
public class Enemy : ShootableObject
{
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private EnemyMover _enemyMover;

    [SerializeField] private float _dieDuration = 2.12f;
    [SerializeField] private float _blinkIntensity = 10.0f;
    [SerializeField] private float _blinkDuration = 0.05f;
    [SerializeField] private float _pickableYOffset = 1f;
    [SerializeField] private Pickable _pickable;
    private float blinkTimer;

    void Start()
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    public override void GetHit()
    {
        blinkTimer = _blinkDuration;
        BlinkOnHit();
        StartCoroutine(DieAfterDuration(_dieDuration));
        _enemyMover.Die();
        DropTempPickable(_pickable);
    }

    private void DropTempPickable(Pickable pickable)
    {
        var pickablePosition = this.transform.position;
        pickablePosition.y += _pickableYOffset;

        var pickableObject = Instantiate(_pickable, pickablePosition, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(pickableObject.gameObject, 5f);
    }

    private void BlinkOnHit()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / _blinkDuration);
        float intensity = (lerp * _blinkIntensity) + 1.0f;
        _skinnedMeshRenderer.material.color = Color.black * intensity;
    }
}
