using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Collider _collider;
    [SerializeField] private EnemyContoller _controller;

    public EnemyContoller Controller => _controller;

    private HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = new HealthSystem(30f, _healthBar);
        _healthBar.OnDie += Die;
    }

    private void Update()
    {
        if (_healthSystem.IsDead) 
            return;

        _controller.Move();
    }

    public virtual void Hit()
    {
        _healthSystem.TakeDamage(10f);

        if (_healthSystem.IsDead)
            Disable();
    }

    private void Disable() 
    {
        _collider.enabled = false;
        Spawner.RemoveEnemy(this);
    }

    public void Die() 
    {
        Destroy(gameObject);
    }
}