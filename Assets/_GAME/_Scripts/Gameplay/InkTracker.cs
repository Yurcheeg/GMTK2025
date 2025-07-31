using UnityEngine;

public class InkTracker : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _inkSlider;
    [SerializeField] private LinePainter _linePainter;

    private void Start() => _inkSlider.value = _inkSlider.maxValue;

    //if more than min amount of ink - you can paint
    private void Update() => _linePainter.CanPaint = _inkSlider.value != _inkSlider.minValue;

    private void OnPointCountIncreased() => _inkSlider.value--;

    private void OnPointCountDescreased() => _inkSlider.value++;

    private void Awake()
    {
        _linePainter.OnPointCountIncreased += OnPointCountIncreased;
        _linePainter.OnPointCountDecreased += OnPointCountDescreased;
    }

    private void OnDestroy()
    {
        _linePainter.OnPointCountIncreased -= OnPointCountIncreased;
        _linePainter.OnPointCountDecreased -= OnPointCountDescreased;
    }
}
