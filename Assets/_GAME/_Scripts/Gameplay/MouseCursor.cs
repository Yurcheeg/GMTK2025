using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2 GetPosition() => transform.position;

    private void SetPosition() => transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    private void Awake()
    {
        Cursor.visible = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetPosition();

        if(Mouse.current.leftButton.wasPressedThisFrame)
            _spriteRenderer.color = Color.green;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            _spriteRenderer.color = Color.blue;
    }
}
