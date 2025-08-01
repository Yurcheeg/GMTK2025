using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    private bool _isPaused;

    private void OnPausePressed()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    private void OnReloadPressed() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    private void Awake()
    {
        _inputHandler.PausePressed += OnPausePressed;
        _inputHandler.ReloadPressed += OnReloadPressed;
    }

    private void OnDestroy()
    {
        _inputHandler.PausePressed -= OnPausePressed;
        _inputHandler.ReloadPressed -= OnReloadPressed;
    }
}
