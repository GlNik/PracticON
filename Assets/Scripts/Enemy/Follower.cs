using UnityEngine;

[ExecuteAlways]
public class Follower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float lerpRate = 5;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpRate);
    }

}
