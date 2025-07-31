using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private LinePainter _linePainter;

    private bool _isPaused = false;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if(Keyboard.current.zKey.wasPressedThisFrame)
            _linePainter.DeletePreviousPoint();

        if (Keyboard.current.enterKey.wasPressedThisFrame)
            StartPlayerMovement();
    }

    private void StartPlayerMovement() => _player.Move();
}