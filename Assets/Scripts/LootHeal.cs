using UnityEngine;

public class LootHeal : MonoBehaviour
{
    [SerializeField] private int healthValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.attachedRigidbody.GetComponent<PlayerHealth>();
        if (player && player.Health < player.MaxHealth)
        {
            player.AddHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
