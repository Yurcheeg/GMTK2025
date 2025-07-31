using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickable : MonoBehaviour
{
    private Collider2D _collider;
    private Player _player;

    protected Collider2D Collider2D => _collider;
    protected Player Player => _player;

    public abstract void PickUp();

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) == false)
            return;

        _player = player;

        PickUp();
    }
}