using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private TakeDamageState _takeDamageAnimation;
    [SerializeField] private MoveState _moveAnimation;
    [SerializeField] private SpawnState _spawnAnimation;

    public Transform Model => _model;

    public SpawnState SpawnAnimation => _spawnAnimation;
    public MoveState MoveAnimation => _moveAnimation;
    public TakeDamageState TakeDamageAnimation => _takeDamageAnimation;

    private AnimationState _currentState;

    private void Update()
    {
        _currentState.Update(this);
    }

    public void SwitchState(AnimationState state) 
    {
        _currentState = state;
        _currentState.Init(this);
    }
}