using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private AudioSource deathSound;

    public UnityEvent EventOnTakeDamage;
    public UnityEvent EventOnDie;

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        healthBar.value = health;
        healthBar.gameObject.SetActive(true);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            EventOnTakeDamage.Invoke();
        }
    }
    public void Die()
    {
        if (deathSound != null)
        {
            SoundDeathPlay();
        }
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        healthBar.gameObject.SetActive(false);
        EventOnDie.Invoke();
        Destroy(gameObject);
    }

    public void SoundDeathPlay()
    {
        deathSound.transform.parent = null;
        deathSound.Play();
        Destroy(deathSound.gameObject, 3f);
    }
}
