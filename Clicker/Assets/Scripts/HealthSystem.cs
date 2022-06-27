using UnityEngine;

public class HealthSystem
{
    private float _maxHealth;
    private float _currentHealth;

    private HealthBar _healthBar;

    public HealthSystem(float health, HealthBar healthBar) 
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;

        _healthBar = healthBar;
    }

    public void TakeDamage(float damage) 
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _healthBar.SetHealth(_maxHealth, _currentHealth);
    }
}