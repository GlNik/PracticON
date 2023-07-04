using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 leftEuler;
    [SerializeField] private Vector3 rightEuler;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform _playerTransform;
    private Vector3 _targetEuler;

    void Start()
    {
        _playerTransform = PlayerMove.Instance.transform;
    }

    void Update()
    {
        if (transform.position.x < _playerTransform.position.x)
        {
            _targetEuler = rightEuler;
        }
        else
        {
            _targetEuler = leftEuler;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), Time.deltaTime * rotationSpeed);
    }
}
