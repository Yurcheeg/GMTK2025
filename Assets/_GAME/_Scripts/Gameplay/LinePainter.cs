using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinePainter : MonoBehaviour
{
    //ugh
    public System.Action OnPointCountIncreased;
    public System.Action OnPointCountDecreased;

    [SerializeField] private GameObject _linePrefab;

    [SerializeField] private float _offset;

    private GameObject _currentLine;

    private EdgeCollider2D _edgeCollider;

    private Stack<LineRenderer> _lines = new();

    private Stack<Vector2> _drawPoints = new();

    public Vector2 MousePosition =>
        Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    public bool CanPaint { get; set; } = true;

    //HACK: idk
    public float Offset => _offset;

    public Stack<Vector2> DrawPoints => _drawPoints;

    /// <summary>
    /// Deletes the previous vertex and/or line, depending on whether or not there are any visible vertices left
    /// </summary>
    public void DeletePreviousPoint()
    {
        if (_lines.Count <= 0)
            return;

        //2 first points are ignored because they are in the same place (negligeable)
        if (_lines.Peek().positionCount <= 2)
        {
            Destroy(_lines.Peek());
            _lines.Pop();

            if (_lines.Count <= 0)
                return;

            _edgeCollider = _lines.Peek().GetComponent<EdgeCollider2D>();
        }

        _lines.Peek().positionCount--;
        OnPointCountDecreased?.Invoke();

        List<Vector2> positions = new();

        for (int i = 0; i < _lines.Peek().positionCount; i++)
            positions.Add(_lines.Peek().GetPosition(i));

        _edgeCollider.points = positions.ToArray();
    }

    public void CreateLine()
    {
        _currentLine = Instantiate(_linePrefab);
        _lines.Push(_currentLine.GetComponent<LineRenderer>());
        _edgeCollider = _currentLine.GetComponent<EdgeCollider2D>();

        _drawPoints.Clear();
        //store the current pos in case of some insane mouse flick
        Vector2 startPosition = MousePosition;

        //mark two points of the line.
        //they would overlap, therefore not noticeable.
        for (int i = 0; i < 2; i++)
        {
            _drawPoints.Push(startPosition);
            _lines.Peek().SetPosition(i, startPosition);
        }

        _edgeCollider.points = _drawPoints.ToArray();
    }

    public void UpdateLine(Vector2 newPoint)
    {
        _drawPoints.Push(newPoint);

        _lines.Peek().positionCount++;
        _lines.Peek().SetPosition(_lines.Peek().positionCount - 1, newPoint);

        OnPointCountIncreased?.Invoke();
        _edgeCollider.points = _drawPoints.ToArray();
    }
}
