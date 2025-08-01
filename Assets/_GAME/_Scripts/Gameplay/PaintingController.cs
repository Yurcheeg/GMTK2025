using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintingController : MonoBehaviour
{
    [SerializeField] private LinePainter _linePainter;
    [SerializeField] private InkTracker _inkTracker;
    [SerializeField] private InputHandler _inputHandler;

    private PaintBlocker[] _blockers;
    private bool _canPaint;
    private bool _isCurrentlyPainting = false;

    public bool LineBlocked => _blockers.Any(b => b.IsBlockingLine);

    private void OnPaintStarted()
    {
        if (_canPaint == false)
            return;

        _linePainter.CreateNewLine();
        _isCurrentlyPainting = true;
    }

    private void OnPaintHeld(Vector2 mousePosition)
    {
        if (_canPaint == false || _isCurrentlyPainting == false)
            return;

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float distance = Vector2.Distance(mouseWorldPosition, _linePainter.DrawPoints.Peek());

        if (distance > _linePainter.Offset)
        {
            _linePainter.AddNewPointToLine(mouseWorldPosition);
            _inkTracker.RemoveInk();

            if (_inkTracker.HasInk == false)
                _isCurrentlyPainting = false;
        }
    }

    private void OnUndoPressed()
    {
        if (_inputHandler.IsPlayMode)
            return;

        _linePainter.UndoLastPoint();
        _inkTracker.AddInk();
    }


    private void Update()
    {
        _canPaint = _inputHandler.IsPlayMode == false
        && LineBlocked == false
        && _inkTracker.HasInk;

        if (_isCurrentlyPainting && _canPaint == false)
            _isCurrentlyPainting = false;
    }

    private void Awake()
    {
        //hack xd
        _blockers = FindObjectsByType<PaintBlocker>(FindObjectsSortMode.None);

        SubscribeToEvents();
    }

    private void OnDestroy() => UnsubscribeFromEvents();

    private void SubscribeToEvents()
    {
        _inputHandler.PaintStarted += OnPaintStarted;
        _inputHandler.PaintHeld += OnPaintHeld;
        _inputHandler.UndoPressed += OnUndoPressed;
    }

    private void UnsubscribeFromEvents()
    {
        _inputHandler.PaintStarted -= OnPaintStarted;
        _inputHandler.PaintHeld -= OnPaintHeld;
        _inputHandler.UndoPressed -= OnUndoPressed;
    }
}
