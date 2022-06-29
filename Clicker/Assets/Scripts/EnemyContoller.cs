using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    public GameArea GameArea { private get; set; }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private Transform _modelTransform;

    private float _accelerationTimer = 0;
    private float _currentMoveSpeed = 0;

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
        {
            _targetPosition = GameArea.GetRandomPosition();
            _accelerationTimer = 0;
        }

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
        _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void SetAcceleration()
    {
        if (_accelerationTimer != _accelerationTime)
        {
            _accelerationTimer += Time.deltaTime;

            float curveValue = _accelerationCurve.Evaluate(_accelerationTimer / _accelerationTime);
            _currentMoveSpeed = Mathf.Lerp(0, _moveSpeed, curveValue);
        }
        else _accelerationTimer = 0;
    }
}