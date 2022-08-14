using System;
using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour, ISwitchable, IVelocity
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _smoothDampTime = 2f;
    [SerializeField] private float _waypointWaitTime = 1f;
    [SerializeField] bool _isMoving;
    [SerializeField] private float _deadZone = 0.01f;

    private Rigidbody2D _ribidBody;
    private WaitForSeconds _waypointWaitTimeWaitForSeconds;
    private int _waypointIndex;
    private Vector3 _targetPosition;
    private Vector3 _currentVelocity;

    public bool IsSwitchedOn => _isMoving;

    public Vector2 Velocity => _currentVelocity;

    private void Awake()
    {
        _ribidBody = GetComponent<Rigidbody2D>();
        _waypointWaitTimeWaitForSeconds = new WaitForSeconds(_waypointWaitTime);
        _waypointIndex = 0;
        _targetPosition = _waypoints[_waypointIndex].position;
        enabled = _isMoving;
    }

    private void Start()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            while (Vector3.Distance(_targetPosition, _ribidBody.position) > _deadZone)
            {
                Vector3 newPosition = Vector3.SmoothDamp(_ribidBody.position, _targetPosition, ref _currentVelocity, _smoothDampTime);
                if (Vector3.Distance(newPosition, _targetPosition) <= _deadZone)
                {
                    newPosition = _targetPosition;
                }
                transform.position = newPosition;
                yield return new WaitForEndOfFrame();
            }

            _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
            _targetPosition = _waypoints[_waypointIndex].position;

            yield return _waypointWaitTimeWaitForSeconds;
        }
    }

    public void SwitchOn()
    {
        SetMovement(true);
        StartMoving();
    }

    public void SwitchOff()
    {
        SetMovement(false);
        StopAllCoroutines();
        _currentVelocity = Vector3.zero;
    }

    private void SetMovement(bool isMoving)
    {
        _isMoving = enabled = isMoving;
    }
}