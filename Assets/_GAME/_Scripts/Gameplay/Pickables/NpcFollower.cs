using UnityEngine;

public class NpcFollower : Pickable
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _textWindow;

    private bool _isFollowing = false;

    public override void PickUp()
    {
        Collider2D.enabled = false;
        
        Follow();
        //todo: maybe set to false when the game starts?
        _textWindow.SetActive(false);
    }

    private void Follow() => _isFollowing = true;
    
    private void Update()
    {
        if (_isFollowing == false)
            return;

        transform.position = Vector2.Lerp(transform.position, Player.transform.position, _speed * Time.deltaTime);
    }
}
