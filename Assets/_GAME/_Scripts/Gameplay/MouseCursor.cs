using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public Vector2 GetPosition() => transform.position;

    private void SetPosition() =>
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    private void Awake()
    {
        //Cursor.visible = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetPosition();

        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            ChangeColor(Color.green);
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            ChangeColor(Color.blue);
    }

    private void ChangeColor(Color color) => _spriteRenderer.color = color;
}
