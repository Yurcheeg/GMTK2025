using UnityEngine;

public class Bowl : MonoBehaviour
{
    public static System.Action GoalReached;
    [SerializeField] private GameStateHandler _gameStateHandler;
    
    private int _loopCount;
    private int _loopTotal;

    private void Awake()
    {
        //hackxd
        _loopTotal = FindObjectsByType<Loop>(FindObjectsSortMode.None).Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Loop loop) == false)
            return;
        
        loop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        _loopCount++;

        if (_loopCount == _loopTotal)
        {
            GoalReached?.Invoke();
            _gameStateHandler.Pause();
        }
    }
}
