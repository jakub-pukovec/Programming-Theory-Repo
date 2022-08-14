using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _jumpTriggered;
    private bool _collidingWithGround;
    private float _horizontalMovement;
    private float _originalScaleX;

    [SerializeField] private float _movementSpeed = 1.5f;
    [SerializeField] private float _jumpForce = 3.5f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] BoxCollider2D _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalScaleX = MathF.Abs(transform.localScale.x);
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpTriggered = true;
        }

        FlipBasedOnHorizontalDirection();
    }

    private void FixedUpdate()
    {
        TreatJumpIfNeeded();

        _rigidbody.velocity = new Vector2(_horizontalMovement * _movementSpeed, _rigidbody.velocity.y);
    }

    private void TreatJumpIfNeeded()
    {
        if (_jumpTriggered)
        {
            _jumpTriggered = false;
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void FlipBasedOnHorizontalDirection()
    {
        if (_horizontalMovement == 0f)
            return;

        Vector3 localScale = transform.localScale;
        localScale.x = _originalScaleX * Mathf.Sign(_horizontalMovement);
        transform.localScale = localScale;
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

        var fruit = collision.GetComponent<Fruit>();
        if (fruit != null)
        {
            fruit.Pickup();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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