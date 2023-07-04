using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
        transform.rotation = Quaternion.identity;
    }
}
