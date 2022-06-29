using UnityEngine;

[System.Serializable]
public class TakeDamageState : AnimationState
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _damagedColor;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;

    private Transform _model;
    private Material _modelMaterial;

    private float _animationTimer = 0;

    public override void Init(AnimatorController animator)
    {
        _animationTimer = 0;
        _model = animator.Model;
        _modelMaterial = _model.GetComponent<MeshRenderer>().material;
        _modelMaterial.color = _damagedColor;
    }

    public override void Update(AnimatorController animator)
    {
        _animationTimer += Time.deltaTime;

        float lerpValue = _animationCurve.Evaluate(_animationTimer / _animationDuration);
        lerpValue = Mathf.Clamp01(lerpValue);

        _model.localScale = Vector3.Lerp(_startScale, _endScale, lerpValue);
        _modelMaterial.color = Color.Lerp(_damagedColor, _startColor, lerpValue);

        if (_animationTimer >= _animationDuration) 
            End(animator);
    }
    public override void End(AnimatorController animator)
    {
        animator.SwitchState(animator.MoveAnimation);
    }
}