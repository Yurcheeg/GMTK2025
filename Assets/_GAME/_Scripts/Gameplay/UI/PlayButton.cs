using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Sprite _playImage;
    [SerializeField] private Sprite _pauseImage;

    private Image _buttonImage;
    [SerializeField] private PlayModeValidator _playModeValidator;
    [SerializeField] private GameStateHandler _gameStateHandler;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();

        _playModeValidator.PlayModeEntered += OnPlayModeEntered;
        _gameStateHandler.Paused += OnPaused;
    }

    private void OnPaused() => _buttonImage.sprite = _playImage;

    private void OnPlayModeEntered()
    {
        _buttonImage.sprite = _pauseImage;
    }

    private void OnDestroy()
    {
        _playModeValidator.PlayModeEntered -= OnPlayModeEntered;
        _gameStateHandler.Paused -= OnPaused;
    }
}
