using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    private int _level;

    public void SetLevel(int level) => _level = level;

    //dont @ me
    public void LoadSelectedLevel() => GoToScene($"Level_{_level + 1}");

    public void GoToScene(string name) => SceneManager.LoadScene(name);
}
