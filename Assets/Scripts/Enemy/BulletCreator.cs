using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private Transform spawn;
    [SerializeField] private float speed = 8f;

    private Rigidbody _rigidbody;
    public void Create()
    {
        Bomb newBullet = Instantiate(bombPrefab, spawn.position, spawn.rotation); //Quaternion.identity);
        _rigidbody = newBullet.GetComponent<Rigidbody>();

        Transform playerTransform = PlayerMove.Instance.transform;
        Vector3 toPlayer = (playerTransform.position - transform.position).normalized;
        _rigidbody.velocity = toPlayer * speed;
    }
}
