using UnityEngine;
using System;

[RequireComponent(typeof(EnemyContoller))]
[RequireComponent(typeof(AnimatorController))]
public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private float _startHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Collider _collider;

    private HealthSystem _healthSystem;
    private EnemyContoller _controller;
    private AnimatorController _animator;

    public EnemyContoller Controller => _controller;

    public static event Action OnEnemyDied;

    private bool _canMove = false;

    private void Awake()
    {
        _animator = GetComponent<AnimatorController>();
        _controller = GetComponent<EnemyContoller>();
    }

    private void Start()
    {
        _healthSystem = new HealthSystem(_startHealth, _healthBar);
        _healthBar.OnFilled += Die;

        _animator.SwitchState(_animator.SpawnAnimation);
    }

    private void Update()
    {
        if (_healthSystem.IsDead && _canMove) 
            return;

        _controller.Move();
    }

    public virtual void Hit()
    {
        _healthSystem.TakeDamage(1f);
        _controller.Slow();
        _animator.SwitchState(_animator.TakeDamageAnimation);

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
        OnEnemyDied?.Invoke();
    }
}