using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    public event Action Paused;
    [SerializeField] private InputHandler _inputHandler;

    public bool IsPlayMode { get; private set; }
    public bool IsPaused { get; private set; }

    public void EnterPlayMode()
    {
        if (IsPlayMode == false)
            IsPlayMode = true;

        IsPaused = false;
        Time.timeScale = 1f;
    }

    public void Restart() => ReloadScene();

    public void Pause() => TogglePause();

    private void TogglePause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        Paused?.Invoke();
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
