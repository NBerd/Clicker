using UnityEngine;

public class HealthSystem
{
    private float _maxHealth;
    private float _currentHealth;

    private HealthBar _healthBar;

    public bool IsDead { get { return _currentHealth == 0; } }

    public HealthSystem(float health, HealthBar healthBar) 
    {
        _maxHealth = Mathf.Round(health + GameManager.GameDifficulty);
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