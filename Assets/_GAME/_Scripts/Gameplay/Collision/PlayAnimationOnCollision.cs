using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayAnimationOnCollision : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const int _triggerId = 0;
    private bool _onCooldown = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Loop _) == false)
            return;

        if (_onCooldown == false)
            _animator.SetTrigger("LoopDropped");
        
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(1f);

        _onCooldown = false;
    }
}
