using UnityEngine;

[System.Serializable]
public class MoveState : AnimationState
{
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private float _animationStepDuration;

    private Transform _model;
    private float _animationTimer;

    private Vector3 _targetScale;
    private Vector3 _currentScale;

    public override void Init(AnimatorController animator)
    {
        _model = animator.Model;
        _currentScale = _startScale;
        _targetScale = _endScale;
    }

    public override void Update(AnimatorController animator)
    {
        _animationTimer += Time.deltaTime;

        if(_animationTimer >= _animationStepDuration) 
        {
            _animationTimer = 0;

            _targetScale = _targetScale == _endScale ? _startScale : _endScale;
            _currentScale = _currentScale == _endScale ? _startScale : _endScale;
        }

        float lerpValue = _animationTimer / _animationStepDuration;

        _model.localScale = Vector3.Lerp(_currentScale, _targetScale, lerpValue);
    }

    public override void End(AnimatorController animator) { }
}
