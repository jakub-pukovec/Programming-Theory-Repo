using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDetectionTolerance = 0.1f;
    [SerializeField] BoxCollider2D _boxCollider;

    private bool _jumpTriggered;

    private bool _collidingWithGround;
    private float _horizontalMovement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalMovement += Input.GetAxis("Horizontal") * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpTriggered = true;
        }
    }

    private void FixedUpdate()
    {
        if (_jumpTriggered)
        {
            _jumpTriggered = false;
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }

        _rigidbody.velocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidbody.velocity.y);
        _horizontalMovement = 0;
    }

    private bool IsGrounded()
    {
        return _collidingWithGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer(collision))
        {
            _collidingWithGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer(collision))
        {
            _collidingWithGround = false;
        }
    }

    private bool IsTouchingGroundLayer(Collider2D triggerCollider)
    {
        return (_groundMask.value & (1 << triggerCollider.gameObject.layer)) > 0;
    }
}