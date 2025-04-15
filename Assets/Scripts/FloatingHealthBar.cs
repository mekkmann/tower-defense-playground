using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private Camera _camera;

    private void Awake()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }
}
