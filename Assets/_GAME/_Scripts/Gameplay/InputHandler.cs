using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private LinePainter _linePainter;

    private bool _isPaused = false;
    private bool _isPlayMode = false;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Pause();

        if (Keyboard.current.rKey.wasPressedThisFrame)
            ReloadScene();

        if (Keyboard.current.enterKey.wasPressedThisFrame)
            EnterPlayMode();

        if (_isPlayMode)
            return;

        HandlePainterInputs();
    }

    private void HandlePainterInputs()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
            _linePainter.DeletePreviousPoint();

        if (_linePainter.CanPaint == false)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _linePainter.CreateLine();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            //small offset in case of accidental missclick
            //big enough to be noticeable when removed
            if (Vector2.Distance(_linePainter.MousePosition, _linePainter.DrawPoints.Peek()) > _linePainter.Offset)
                _linePainter.UpdateLine(_linePainter.MousePosition);
        }
    }

    private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void Pause()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    //no need for exit because of restart button
    private void EnterPlayMode()
    {
        _isPlayMode = true;
        _linePainter.CanPaint = false;
        StartPlayerMovement();
    }

    //no need for stop because of restart button
    private void StartPlayerMovement() => _player.Move();
}