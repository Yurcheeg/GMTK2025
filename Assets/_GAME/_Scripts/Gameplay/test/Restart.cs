using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.rKey.isPressed)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
