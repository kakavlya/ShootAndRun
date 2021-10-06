using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string IsDeadConst = "IsDead";

    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Transform _spineToRotate;
    [SerializeField] private float _positiveXBound;
    [SerializeField] private float _negativeXBound;


    private Quaternion _rotation;
    private Rigidbody _body;
    private Animator _animator;

    public float HorizontalSpeed { get => _horizontalSpeed; set => _horizontalSpeed = value; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _rotation = new Quaternion(0f,0f,0f,0f);
        Reset();
    }

    private void Update()
    {
        float h = Input.GetAxis(HorizontalAxis);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.touches[0];
            h = touch.deltaPosition.x;
        }

        Vector3 movement = new Vector3(h, 0f, _movementSpeed);
        CheckBounds();
        transform.Translate(movement * Time.deltaTime * HorizontalSpeed);

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.forward);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    private void CheckBounds()
    {
        Vector3 currentPost = transform.position;

        if (currentPost.x > _positiveXBound)
            currentPost.x = _positiveXBound;

        if (currentPost.x < _negativeXBound)
            currentPost.x = _negativeXBound;

        transform.position = currentPost;
    }

    public void Reset()
    {
        _body.transform.position = _startPosition;
        _body.transform.rotation = _rotation;
        _body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionX;
        _animator.SetBool(IsDeadConst, false);
    }

    public void Die()
    {
        _animator.SetBool(IsDeadConst, true);
    }
}
