using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Left,
    Right
}

public class Walker : MonoBehaviour
{
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Transform rightTarget;

    [SerializeField] private float speed = 3;
    [SerializeField] private float stopTime = 0.5f;

    private bool _isStopped;

    public Direction CurrentDirecton;

    public UnityEvent EventOnLeftTarget;
    public UnityEvent EventOnRightTarget;

    [SerializeField] private Transform rayStart;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        leftTarget.parent = null;
        rightTarget.parent = null;
    }

    void Update()
    {
        if (_isStopped == true)
        {
            return;
        }
        if (CurrentDirecton == Direction.Left)
        {
            transform.position -= new Vector3(Time.deltaTime * speed, 0f, 0f);
            if (transform.position.x < leftTarget.position.x)
            {
                CurrentDirecton = Direction.Right;
                _isStopped = true;
                Invoke(nameof(ContinueWalk), stopTime);
                EventOnLeftTarget.Invoke();
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * speed, 0f, 0f);
            if (transform.position.x > rightTarget.position.x)
            {
                CurrentDirecton = Direction.Left;
                _isStopped = true;
                Invoke(nameof(ContinueWalk), stopTime);
                EventOnRightTarget.Invoke();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(rayStart.position, Vector3.down, out hit, 300, layerMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point;
        }
    }

    private void ContinueWalk()
    {
        _isStopped = false;
    }

}
