using UnityEngine;

public class TakeDamageCollider : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private bool dieOnAnyCollision = false;

    public void TakeDamage(int value)
    {
        enemyHealth.TakeDamage(value);
        if (dieOnAnyCollision)
        {
            enemyHealth.Die();
        }
    }
}
