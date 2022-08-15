using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _jumpTriggered;
    private bool _isGrounded;
    private float _horizontalMovement;
    private float _originalScaleX;
    private Animator _animator;

    [SerializeField] private float _movementSpeed = 1.5f;
    [SerializeField] private float _jumpForce = 3.5f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] BoxCollider2D _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalScaleX = MathF.Abs(transform.localScale.x);
        _animator = GetComponent<Animator>();
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

        Animate();
    }

    private void Animate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat("DropSpeed", _rigidbody.velocity.y);
        Debug.Log(_rigidbody.velocity.y);
    }

    private void TreatJumpIfNeeded()
    {
        if (_jumpTriggered)
        {
            _jumpTriggered = false;
            if (_isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _animator.SetBool("Jump", true);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer(collision))
        {
            _isGrounded = true;
            _animator.SetBool("Jump", false);
        }

        var fruit = collision.GetComponent<Fruit>();
        if (fruit != null)
        {
            fruit.Pickup();
        }

        if (collision.CompareTag("VerticalPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer(collision))
        {
            _isGrounded = true;
        }

        if (collision.CompareTag("Platform"))
        {
            IVelocity platformVelocity = collision.GetComponent<IVelocity>();
            if (platformVelocity != null)
            {
                //Debug.Log(platformVelocity.Velocity);
                _rigidbody.AddForce(platformVelocity.Velocity / Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTouchingGroundLayer(collision))
        {
            _isGrounded = false;
        }

        if (collision.CompareTag("VerticalPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private bool IsTouchingGroundLayer(Collider2D triggerCollider)
    {
        return (_groundMask.value & (1 << triggerCollider.gameObject.layer)) > 0;
    }
}