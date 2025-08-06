using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUnlockedLevels : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;

    private int _unlockedLevelCount;

    private void Start()
    {
        //PlayerPrefs.SetInt("levels_unlocked", 0);
        _unlockedLevelCount = PlayerPrefs.GetInt("levels_unlocked", 0);
        print(_unlockedLevelCount);

        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_unlockedLevelCount >= i)
                _buttons[i].gameObject.SetActive(true);
            else
                _buttons[i].gameObject.SetActive(false);
        }
    }
}
