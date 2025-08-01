using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    public bool IsPlayMode { get; private set; }
    public bool IsPaused { get; private set; }

    public void EnterPlayMode()
    {
        if (IsPlayMode == false)
            IsPlayMode = true;
    }

    public void Restart() => ReloadScene();

    private void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }
    private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void Awake()
    {
        _inputHandler.PausePressed += TogglePause;
        _inputHandler.ReloadPressed += ReloadScene;
    }

    private void OnDestroy()
    {
        _inputHandler.PausePressed -= TogglePause;
        _inputHandler.ReloadPressed -= ReloadScene;
    }
}
