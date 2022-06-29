using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Image _fillBarImage;

    [SerializeField] private float _fillDelay;
    [SerializeField] private float _fadeDelay;
    [SerializeField] private float _fadingTime;
    [SerializeField] private float _fillSpeedMultiplier;
    [SerializeField] private AnimationCurve _fillCurve;
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _fadingTimer;
    private float _lastInteractionTime;
    private Coroutine _fillEffect;

    public event Action OnFilled;

    private void Update()
    {
        if (Time.time > _lastInteractionTime + _fadeDelay && _canvasGroup.alpha > 0) 
        {
            _fadingTimer += Time.deltaTime;
            float fadeValue = Mathf.Lerp(1, 0, _fadingTimer / _fadingTime);

            Fade(fadeValue);
        }
        else _fadingTimer = 0;
    }

    private void Fade(float value) 
    {
        _canvasGroup.alpha = value;
    }

    public void SetHealth(float maxValue, float currentValue) 
    {
        float fillValue = currentValue / maxValue;

        _healthBarImage.fillAmount = fillValue;
        _lastInteractionTime = Time.time;

        if (_fillEffect != null)
            StopCoroutine(_fillEffect);

        _fillEffect = StartCoroutine(FillEffect(fillValue));

        Fade(1);
    }

    IEnumerator FillEffect(float healthValue) 
    {
        float startFillValue = _fillBarImage.fillAmount;
        float fillValue = startFillValue;

        float timer = 0f;
        float fillingTime = (fillValue - healthValue) / _fillSpeedMultiplier;

        while (fillValue != healthValue) 
        {
            if (Time.time >= _lastInteractionTime + _fillDelay) 
            {
                timer += Time.deltaTime;

                float curveValue = _fillCurve.Evaluate(timer / fillingTime);
                fillValue = Mathf.Lerp(startFillValue, healthValue, curveValue);

                _fillBarImage.fillAmount = fillValue;
            }

            yield return null;
        }

        if (healthValue <= 0)
            OnFilled?.Invoke();
    }
}