using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinePainter : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;

    [SerializeField] private float _offset;

    private GameObject _currentLine;

    private EdgeCollider2D _edgeCollider;

    private Stack<LineRenderer> _lines = new();

    private Stack<Vector2> _drawPoints = new();

    private Vector2 MousePosition =>
        Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CreateLine();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            //small offset in case of accidental missclick
            if (Vector2.Distance(MousePosition, _drawPoints.Peek()) > _offset)
                UpdateLine(MousePosition);
        }

        //HACK: Test implementation. Replace
        if (Keyboard.current.zKey.wasPressedThisFrame)
            DeletePreviousPoint();
    }

    private void CreateLine()
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

    private void UpdateLine(Vector2 newPoint)
    {
        _drawPoints.Push(newPoint);

        _lines.Peek().positionCount++;
        _lines.Peek().SetPosition(_lines.Peek().positionCount - 1, newPoint);

        _edgeCollider.points = _drawPoints.ToArray();
    }

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

        List<Vector2> temp = new();

        for (int i = 0; i < _lines.Peek().positionCount; i++)
            temp.Add(_lines.Peek().GetPosition(i));

        _edgeCollider.points = temp.ToArray();
    }
}
