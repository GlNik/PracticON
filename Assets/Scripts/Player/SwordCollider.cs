using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] private int damageValue;
    public int GetDamage => damageValue;

    private void OnTriggerEnter(Collider other)
    {
        TakeDamageCollider takeDamageCollider = other.gameObject.GetComponent<TakeDamageCollider>();
        if (takeDamageCollider)
        {
            takeDamageCollider.TakeDamage(damageValue);
        }
    }

}
