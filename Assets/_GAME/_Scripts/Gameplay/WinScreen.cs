using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    private RectTransform _rectTransform;

    private float _height = 700f;
    private float _duration = 1.5f;
    private Vector2 _originalPosition;
    private Vector2 _hiddenPosition;

    private void OnGoalReached()
    {
        _image.gameObject.SetActive(true);

        _rectTransform.DOKill();

        _rectTransform.anchoredPosition = _hiddenPosition;

        _rectTransform
            .DOAnchorPos(_originalPosition, _duration)
            .SetEase(Ease.OutBack);
    }

    private void Awake()
    {
        Bowl.GoalReached += OnGoalReached;

        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
        _hiddenPosition = _originalPosition + Vector2.up * _height;
    }

    private void OnDestroy()
    {
        Bowl.GoalReached -= OnGoalReached;
        _rectTransform.DOKill();
    }

    private void Start()
    {
        _image.gameObject.SetActive(false);

        _rectTransform.anchoredPosition = _hiddenPosition;
        Invoke(nameof(OnGoalReached), 1f);
    }
}
