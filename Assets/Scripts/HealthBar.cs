using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _slider = GetComponent<Slider>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    #region Public Methods
    public void UpdateBar(float currentValue, float maxValue)
    {
        UpdateSlider(currentValue, maxValue);
        UpdateText(currentValue, maxValue);
    }
    #endregion

    #region Private Methods
    private void UpdateSlider(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }
    private void UpdateText(float currentValue, float maxValue)
    {
        _text.text = $"{currentValue} / {maxValue}";
    }
    #endregion
}
