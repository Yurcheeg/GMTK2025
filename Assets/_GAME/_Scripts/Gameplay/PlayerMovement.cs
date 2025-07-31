using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current.dKey.isPressed)
            _rigidbody.AddForce(_speed * Time.deltaTime * Vector2.right,ForceMode2D.Impulse);
        if(Keyboard.current.aKey.isPressed)
            _rigidbody.AddForce(_speed * Time.deltaTime * Vector2.left,ForceMode2D.Impulse);
    }
}
