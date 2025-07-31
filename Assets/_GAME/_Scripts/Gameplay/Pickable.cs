using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public abstract class Pickable : MonoBehaviour
{
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    public abstract void PickUp();
}