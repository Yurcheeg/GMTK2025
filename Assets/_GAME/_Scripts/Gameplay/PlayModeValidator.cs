using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayModeValidator : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Player _player;
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private PaintBlocker[] _blockers;
    
    private void OnPlayModePressed()
    {
        if (CanEnterPlayMode())
        {
            _gameStateHandler.EnterPlayMode();
            _player.Move();
        }
        else //todo: replace with a popup
            Debug.LogWarning("Cannot enter playmode - invalid lines present");
    }

    private bool CanEnterPlayMode() => _blockers.Any(b => b.IsBlockingLine) == false;

    private void Start() => _blockers = FindObjectsByType<PaintBlocker>(FindObjectsSortMode.None);//hack xd

    private void Awake() => _inputHandler.PlayModePressed += OnPlayModePressed;

    private void OnDestroy() => _inputHandler.PlayModePressed -= OnPlayModePressed;
}