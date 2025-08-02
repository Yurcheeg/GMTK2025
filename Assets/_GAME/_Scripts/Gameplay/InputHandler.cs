using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event System.Action PausePressed;
    public event System.Action ReloadPressed;
    public event System.Action PlayPressed;
    public event System.Action UndoPressed;
    public event System.Action PaintStarted;
    public event System.Action<Vector2> PaintHeld;

    [SerializeField] private UnityEngine.UI.Button _playButton;
    [SerializeField] private UnityEngine.UI.Button _reloadButton;
    //[SerializeField] private UnityEngine.UI.Button _muteButton;
    [SerializeField] private UnityEngine.UI.Button _undoButton;
    [Space]
    [SerializeField] private GameStateHandler _gameStateHandler;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            if (_gameStateHandler.IsPaused)
                PlayPressed?.Invoke();
            else
                PausePressed?.Invoke();
        });
        _reloadButton.onClick.AddListener(() => PausePressed?.Invoke());
        //_muteButton.onClick.AddListener(() => PausePressed?.Invoke());
        _undoButton.onClick.AddListener(() => UndoPressed?.Invoke());
    }

    private void Update()
    {
        #region Game State Input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            PausePressed?.Invoke();

        if (Keyboard.current.rKey.wasPressedThisFrame)
            ReloadPressed?.Invoke();

        if (Keyboard.current.enterKey.wasPressedThisFrame)
            PlayPressed?.Invoke();
        #endregion

        #region Paint Input
        if (Keyboard.current.zKey.wasPressedThisFrame)
            UndoPressed?.Invoke();

        if (Mouse.current.leftButton.wasPressedThisFrame)
            PaintStarted?.Invoke();
        else if (Mouse.current.leftButton.isPressed)
            PaintHeld?.Invoke(Mouse.current.position.ReadValue());
        #endregion
    }
}