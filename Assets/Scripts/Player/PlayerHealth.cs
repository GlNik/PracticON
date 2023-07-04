using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5; public int Health => health;
    [SerializeField] private int maxHealth = 8; public int MaxHealth => maxHealth;


    [SerializeField] private AudioSource addHealthSound;
    [SerializeField] private HealthUI healthUI;

    private bool _invulnerable = false;
    private bool _die;

    public UnityEvent EventOnTakeDamage;
    public UnityEvent EventDie;

    private void Start()
    {
        healthUI.Setup(maxHealth);
        healthUI.DisplayHealth(health);
    }

    public void TakeDamage(int damageValue)
    {
        if (_invulnerable == false)
        {
            health -= damageValue;
            if (health <= 0)
            {
                health = 0;
                if (_die == false)
                {
                    Die();
                    _die = true;
                }
            }
            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
            healthUI.DisplayHealth(health);
            EventOnTakeDamage.Invoke();
        }
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void AddHealth(int healthValue)
    {
        health += healthValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        addHealthSound.Play();
        healthUI.DisplayHealth(health);
    }

    private void Die()
    {
        Invoke(nameof(OpenLoseWindow), 2f);
        EventDie.Invoke();
    }

    private void OpenLoseWindow()
    {
        WindowManager.Instance.Lose();
    }
}
