using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    public GameArea GameArea { private get; set; }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _slowMultiplier;
    [SerializeField] private float _slowDuration;
    [SerializeField] private Transform _modelTransform;

    private float _currentMoveSpeed;

    private float _slowTimer = 0;

    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = GameArea.GetRandomPosition();
        _modelTransform.rotation = GetLookRotation();

        _moveSpeed += GameManager.GameDifficulty;
        _rotationSpeed += GameManager.GameDifficulty;
    }

    public void Move()
    {
        SetAcceleration();
        Rotate(GetLookRotation());

        Vector3 currentPosition = transform.position;

        if (currentPosition == _targetPosition) 
            _targetPosition = GameArea.GetRandomPosition();

        transform.position = Vector3.MoveTowards(currentPosition, _targetPosition, _currentMoveSpeed * Time.deltaTime);
    }

    private Quaternion GetLookRotation()
    {
        Vector3 diraction = (_targetPosition - transform.position).normalized;
        Quaternion lookRotation = diraction != Vector3.zero ? Quaternion.LookRotation(diraction) : Quaternion.identity;

        return lookRotation;
    }

    private void Rotate(Quaternion rotation) 
    {
        float rotationSpeed = _slowTimer > 0 ? _rotationSpeed * _slowMultiplier : _rotationSpeed;

        _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void SetAcceleration()
    {
        if (_slowTimer > 0) 
        {
            _slowTimer -= Time.deltaTime;

            return;
        }

        float speed = Mathf.Lerp(_currentMoveSpeed, _moveSpeed, _accelerationSpeed * Time.deltaTime);

        _currentMoveSpeed = speed;
    }

    public void Slow() 
    {
        float moveSpeed = _moveSpeed * _slowMultiplier;

        moveSpeed = Mathf.Clamp(moveSpeed, 0, _currentMoveSpeed);

        _currentMoveSpeed = moveSpeed;
        _slowTimer = _slowDuration;
    }
}