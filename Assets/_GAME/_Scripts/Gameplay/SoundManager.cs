using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _popSfxSource;
    private bool isMuted;

    public static SoundManager Instance { get; private set; }

    public bool IsMuted
    {
        get => isMuted;
        set
        {
            isMuted = value;
            _musicSource.mute = value;
            _popSfxSource.mute = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlaySong();
    }

    private void PlaySong()
    {
        _musicSource.Play();
    }

    private void OnDrop()
    {
        float originalPitch = _popSfxSource.pitch;

        _popSfxSource.pitch = UnityEngine.Random.Range(-0.2f, 0.2f);
        _popSfxSource.Play();

        _popSfxSource.pitch = originalPitch;
    }

    private void OnWin()
    {
        //_winSfx.Play();
    }

    private void OnEnable()
    {
        Bowl.GoalReached += OnWin;
        Bowl.Dropped += OnDrop;
    }

    private void OnDisable()
    {
        Bowl.GoalReached -= OnWin;
        Bowl.Dropped -= OnDrop;
    }
}
