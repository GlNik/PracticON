using UnityEngine;

public class SwordActivator : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    //anim event
    public void SwordColliderActivate()
    {
        swordCollider.enabled = true;
    }

    //anim event
    public void SwordColliderDiactivate()
    {
        swordCollider.enabled = false;
    }
}
