using UnityEngine;

public class TakeDamageOnEnter : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private bool dieOnAnyCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        Touch(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        Touch(other);
    }

    private void Touch(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            if (collider.attachedRigidbody.GetComponent<SwordCollider>() is SwordCollider sword)
            {
                enemyHealth.TakeDamage(sword.GetDamage);
            }
        }

        if (dieOnAnyCollision)
        {
            if (collider.isTrigger == false)
            {
                enemyHealth.Die();
            }
        }
    }
}
