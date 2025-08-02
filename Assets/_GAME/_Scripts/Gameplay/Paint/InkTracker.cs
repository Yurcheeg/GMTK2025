using UnityEngine;

public class InkTracker : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _inkSlider;

    public bool HasInk => _inkSlider.value > _inkSlider.minValue;

    public void AddInk() => _inkSlider.value = Mathf.Min(++_inkSlider.value, _inkSlider.maxValue);

    public void RemoveInk() => _inkSlider.value = Mathf.Max(--_inkSlider.value, _inkSlider.minValue);

    private void Start() => _inkSlider.value = _inkSlider.maxValue;
}
