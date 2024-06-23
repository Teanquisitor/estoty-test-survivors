using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public UnityAction<float> OnHealthChanged;
    public UnityAction OnDied;

    private void Awake() => currentHealth = maxHealth;

    public void Initialize(int health)
    {
        maxHealth = health;
        currentHealth = health;
    }

    public void ResetHealth() => currentHealth = maxHealth;

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HandleDeath();
        }

        var healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);
    }

    private void HandleDeath() => OnDied?.Invoke();

}