using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.attachedRigidbody.GetComponent<PlayerHealth>();
        if (player)
        {
            CoinManager.Instance.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
