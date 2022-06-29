using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private float _startHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Collider _collider;
    [SerializeField] private EnemyContoller _controller;

    public EnemyContoller Controller => _controller;

    private HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = new HealthSystem(_startHealth, _healthBar);
        _healthBar.OnFilled += Die;
    }

    private void Update()
    {
        if (_healthSystem.IsDead) 
            return;

        _controller.Move();
    }

    public virtual void Hit()
    {
        _healthSystem.TakeDamage(1f);
        _controller.Slow();

        if (_healthSystem.IsDead)
            Disable();
    }

    public void Disable() 
    {
        _collider.enabled = false;
        Spawner.RemoveEnemy(this);
    }

    public void Die() 
    {
        Destroy(gameObject);
    }
}