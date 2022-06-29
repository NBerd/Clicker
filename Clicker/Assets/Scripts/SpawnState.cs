using UnityEngine;

[System.Serializable]
public class SpawnState : AnimationState
{
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;

    private Transform _model;
    private float _animationTimer;

    public override void Init(AnimatorController animator)
    {
        _model = animator.Model;
        _model.localScale = _startScale;
    }

    public override void Update(AnimatorController animator)
    {
        _animationTimer += Time.deltaTime;

        float lerpValue = _animationCurve.Evaluate(_animationTimer / _animationDuration);
        lerpValue = Mathf.Clamp01(lerpValue);

        _model.localScale = Vector3.Lerp(_startScale, _endScale, lerpValue);

        if (_animationTimer >= _animationDuration) 
            End(animator);
    }

    public override void End(AnimatorController animator)
    {
        animator.SwitchState(animator.MoveAnimation);
    }
}