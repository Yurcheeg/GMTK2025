using UnityEngine;

public class NpcFollower : Pickable
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _textWindow;
    [SerializeField] private Bowl _bowl;

    private bool _isFollowing = false;

    private void Start()
    {
        _bowl = FindAnyObjectByType<Bowl>();
    }

    public override void PickUp()
    {
        //Collider2D.enabled = false;

        Collider2D.isTrigger = false;
        Collider2D.excludeLayers = LayerMask.GetMask("Player");
        Follow();
        //todo: maybe set to false when the game starts?
        _textWindow.SetActive(false);
    }

    private void Follow() => _isFollowing = true;

    private void Update()
    {
        if (_isFollowing == false)
            return;


        if (Player == null)
            MoveTowards(_bowl.transform.position);
        else
            MoveTowards(Player.transform.position);
    }

    private void MoveTowards(Vector2 position)
    {
        transform.position = Vector2.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}
