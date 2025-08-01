using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public event System.Action PausePressed;
    public event System.Action ReloadPressed;
    public event System.Action PlayModePressed;
    public event System.Action UndoPressed;
    public event System.Action PaintStarted;
    public event System.Action<Vector2> PaintHeld;

    [SerializeField] private Player _player;

    public bool IsPlayMode { get; private set; }

    private void Update()
    {
        #region Game State Input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            PausePressed?.Invoke();

        if (Keyboard.current.rKey.wasPressedThisFrame)
            ReloadPressed?.Invoke();

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            //hack
            _player.Move();
            IsPlayMode = true;
            PlayModePressed?.Invoke();
        }
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