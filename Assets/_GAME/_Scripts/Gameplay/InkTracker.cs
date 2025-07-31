using UnityEngine;

public class InkTracker : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _inkSlider;
    [SerializeField] private LinePainter _linePainter;

    private void Start()
    {
        _inkSlider.value = _inkSlider.maxValue;        
    }

    private void Update()
    {
        if (_inkSlider.value == _inkSlider.minValue)
            _linePainter.CanPaint = false;
        else
            _linePainter.CanPaint = true;
    }

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
