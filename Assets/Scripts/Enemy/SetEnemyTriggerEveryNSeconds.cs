using UnityEngine;

public class SetEnemyTriggerEveryNSeconds : MonoBehaviour
{
    [SerializeField] private float period = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "Attack";
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > period)
        {
            _timer = 0;
            animator.SetTrigger(triggerName);
        }
    }
}
