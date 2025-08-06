using System;
using System.Linq;
using UnityEngine;

public class PlayModeValidator : MonoBehaviour
{
    public event Action PlayModeEntered;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Player _player;
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private PaintBlocker[] _blockers;
    private bool _levelCompleted;

    private void OnPlayModePressed()
    {
        if(_levelCompleted) 
            return;

        if (CanEnterPlayMode())
        {
            _gameStateHandler.EnterPlayMode();
            _player.Move();
            PlayModeEntered?.Invoke();
        }
        else //todo: replace with a popup
            Debug.LogWarning("Cannot enter playmode - invalid lines present");
    }

    private void OnGoalReached() => _levelCompleted = true;

    private bool CanEnterPlayMode() => _blockers.Any(b => b.IsBlockingLine) == false;

    private void Start() => _blockers = FindObjectsByType<PaintBlocker>(FindObjectsSortMode.None);//hack xd

    private void Awake()
    {
        _inputHandler.PlayPressed += OnPlayModePressed;
        Bowl.GoalReached += OnGoalReached;
    }

    private void OnDestroy()
    {
        _inputHandler.PlayPressed -= OnPlayModePressed;
        Bowl.GoalReached -= OnGoalReached;
    }

}