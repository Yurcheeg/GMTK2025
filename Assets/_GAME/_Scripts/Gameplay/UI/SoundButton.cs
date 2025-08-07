using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _muteImage;
    [SerializeField] private Sprite _unmuteImage;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _muteImage;
    }

    public void ToggleMute()
    {
        SoundManager.Instance.IsMuted = !SoundManager.Instance.IsMuted;
        if(SoundManager.Instance.IsMuted )
            _image.sprite = _unmuteImage;
        else
            _image.sprite = _muteImage;
    }
}
