using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float Z_POSITION = -10f;

    [SerializeField] private Transform _target;
    [SerializeField] private float _leftBoundary = 0f;
    [SerializeField] private float _rightBoundary = 7.59f;
    [SerializeField] private float _topBoundary = 5f;
    [SerializeField] private float _bottomBoundary = -0.5f;

    private Vector3 _offset;


    private void Awake()
    {
        _offset = _target.position - _target.position + Vector3.back * Z_POSITION;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = _target.position - _offset;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, _leftBoundary, _rightBoundary);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, _bottomBoundary, _topBoundary);
        transform.position = cameraPosition;
    }
}