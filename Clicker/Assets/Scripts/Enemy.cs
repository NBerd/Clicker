using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private HealthBar _healthBar;

    private HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = new HealthSystem(100f, _healthBar);
        _healthBar.OnDie += Die;
    }

    public virtual void Hit()
    {
        _healthSystem.TakeDamage(10f);
    }

    public void Die() 
    {
        Destroy(gameObject);
    }
}