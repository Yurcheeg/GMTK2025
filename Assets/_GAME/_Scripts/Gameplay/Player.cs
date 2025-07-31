using UnityEngine;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class Player: MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    /// <summary>
    /// Sets Rigidbody to Dynamic, allowing it to be moved by gravity
    /// </summary>
    public void Move() => _rigidbody.bodyType = RigidbodyType2D.Dynamic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Pickable pickable))
            pickable.PickUp();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }
}
