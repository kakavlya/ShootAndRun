using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight = 1.0f;
}

public class PlayerWeaponIk : MonoBehaviour
{

    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _aimTransform;
    [Range(0,1)]
    [SerializeField] private float _weight;
    [SerializeField] private float _angleLimit;
    [SerializeField] private float _distanceLimit;
    [SerializeField] private Vector3 _targetOffset;
    [SerializeField] private HumanBone[] _humanBones;
    [SerializeField] private bool _shouldTargetCursor = true;

    private Transform[] _boneTransforms;


    private void Start()
    {
        InitBones();
    }

    private void InitBones()
    {
        Animator animator = GetComponent<Animator>();
        _boneTransforms = new Transform[_humanBones.Length];
        for (int i = 0; i < _boneTransforms.Length; i++)
        {
            _boneTransforms[i] = animator.GetBoneTransform(_humanBones[i].bone);
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetDirection;
        if (_shouldTargetCursor)
            targetDirection = TargetMouseCursor();
        else
            targetDirection = (_targetTransform.position + _targetOffset) - _aimTransform.position;

        Vector3 aimDirection = _aimTransform.forward;
        float blendout = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > _angleLimit)
        {
            blendout += (targetAngle - _angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < _distanceLimit)
        {
            blendout += _distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendout);
        return _aimTransform.position + direction;
    }

    private Vector3 TargetMouseCursor()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        mousePosScreen.z = gameObject.transform.position.z + 5f;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector3 targetDirection = (mousePosWorld + _targetOffset) - _aimTransform.position;
        return targetDirection;
    }

    private void LateUpdate()
    {
        if(_aimTransform == null || _targetTransform == null)
        {
            return;
        }
        
        Vector3 targetPosistion = GetTargetPosition();
        for (int b = 0; b < _boneTransforms.Length; b++)
        {
            Transform bone = _boneTransforms[b];
            float boneWeight = _humanBones[b].weight * _weight;
            AimAtTarget(bone, targetPosistion, boneWeight);
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosistion, float weight)
    {
        Vector3 aimDirection = _aimTransform.forward;
        Vector3 targetDirection = targetPosistion - _aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform target)
    {
        _targetTransform = target;
    }

    public void SetAimTransform(Transform aim)
    {
        _aimTransform = aim;
    }
}
