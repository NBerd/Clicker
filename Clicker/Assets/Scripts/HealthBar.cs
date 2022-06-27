using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Image _fillBarImage;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _fillDelay;
    [SerializeField] private AnimationCurve _fillCurve;

    private float _lastInteractionTime;
    private Coroutine _fillEffect;

    public event Action OnDie;

    public void SetHealth(float maxValue, float currentValue) 
    {
        float fillValue = currentValue / maxValue;

        _healthBarImage.fillAmount = fillValue;
        _lastInteractionTime = Time.time;

        if (_fillEffect != null)
            StopCoroutine(_fillEffect);

        _fillEffect = StartCoroutine(FillEffect(fillValue));
    }

    IEnumerator FillEffect(float healthValue) 
    {
        float startFillValue = _fillBarImage.fillAmount;
        float fillValue = startFillValue;

        float timer = 0f;
        float fillingTime = fillValue - healthValue;

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
            OnDie?.Invoke();
    }
}