using UnityEngine;

public class InkTracker : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _inkSlider;
    [SerializeField] private LinePainter _linePainter;

    public bool HasInk { get; private set; }

    private void Start() => _inkSlider.value = _inkSlider.maxValue;

    //if more than min amount of ink - you can paint
    private void Update() => HasInk = _inkSlider.value != _inkSlider.minValue;

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
