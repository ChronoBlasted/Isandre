using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider sliderWhite;
    [SerializeField] TMP_Text sliderValue;

    Tweener _fillTween;
    Tweener _fillWhiteTween;

    public Tweener FillTween { get => _fillTween; }
    public Tweener FillWhiteTween { get => _fillWhiteTween; }

    public void Init(float value, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = value;

        sliderWhite.value = value;
        sliderWhite.maxValue = maxValue;
    }

    public void Init(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        sliderWhite.maxValue = value;
        sliderWhite.value = value;
    }

    public void SetValue(float newValue)
    {
        slider.value = newValue;
        sliderWhite.value = newValue;
    }

    public void SetMaxValue(float newValue)
    {
        slider.maxValue = newValue;
        sliderWhite.maxValue = newValue;
    }

    public void SetValueSmooth(float newValue, float duration = 0.2f, Ease ease = Ease.OutCirc)
    {
        if (_fillTween != null)
        {
            _fillTween.Kill(true);
            _fillTween = null;
        }

        if (_fillWhiteTween != null)
        {
            _fillWhiteTween.Kill();
            _fillWhiteTween = null;
        }

        _fillTween = slider.DOValue(newValue, duration).SetEase(ease);

        _fillWhiteTween = sliderWhite.DOValue(newValue, duration).SetEase(Ease.Linear);
    }

    public float GetValue()
    {
        return slider.value;
    }


    #region TextUpdateOnValueChange
    public void UpdateTextWithSlash() => sliderValue.text = Mathf.RoundToInt(slider.value) + "/" + slider.maxValue; // Pour l'inspecteur onchange du slider
    public void UpdateTextValue() => sliderValue.text = UIManager.GetFormattedInt(Mathf.RoundToInt(slider.value)).ToString(); // Pour l'inspecteur onchange du slider
    public void UpdateTextValueWithSuffixe(string suffixe) => sliderValue.text = Mathf.RoundToInt(slider.value) + suffixe; // Pour l'inspecteur onchange du slider
    public void UpdateTextValueWithPrefix(string prefix) => sliderValue.text = prefix + Mathf.RoundToInt(slider.value); // Pour l'inspecteur onchange du slider
    public void UpdateText(string prefix = "", string suffixe = "", bool slash = false) => sliderValue.text = prefix + Mathf.RoundToInt(slider.value) + (slash ? "/" + slider.maxValue : "") + suffixe; // Cas precis
    #endregion
}