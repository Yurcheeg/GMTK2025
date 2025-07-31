using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinePainter : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    
    [SerializeField] private float _offset = 0.1f;

    private GameObject _currentLine;

    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider;

    private List<Vector2> _drawPoints = new();

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
            if (Vector2.Distance(MousePosition, _drawPoints[^1]) > _offset)
                UpdateLine(MousePosition);
        }
    }

    private void CreateLine()
    {
        _currentLine = Instantiate(_linePrefab);

        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _edgeCollider = _currentLine.GetComponent<EdgeCollider2D>();

        _drawPoints.Clear();
        //store the current pos in case of some insane mouse flick
        Vector2 startPosition = MousePosition;

        //mark two points of the line.
        //they would overlap, therefore not noticeable.
        for (int i = 0; i < 2; i++)
        {
            _drawPoints.Add(startPosition);
            _lineRenderer.SetPosition(i, startPosition);
        }

        _edgeCollider.points = _drawPoints.ToArray();
    }

    private void UpdateLine(Vector2 newPoint)
    {
        _drawPoints.Add(newPoint);

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPoint);

        _edgeCollider.points = _drawPoints.ToArray();
    }
}
