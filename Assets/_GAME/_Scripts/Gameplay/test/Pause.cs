using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private bool _isPaused = false;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
        }
    }
}
