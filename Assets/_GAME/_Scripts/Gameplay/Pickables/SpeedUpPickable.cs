using UnityEngine;

public class SpeedUpPickable : Pickable
{
    [SerializeField] private float _speed;

    public override void PickUp()
    {
        Vector2 forceDirection = transform.position - Player.transform.position;
        Player.Push(forceDirection, _speed);

        Destroy(gameObject);
    }
}
