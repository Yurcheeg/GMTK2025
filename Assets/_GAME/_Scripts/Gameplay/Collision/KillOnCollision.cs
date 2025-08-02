using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillOnCollision : MonoBehaviour
{
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private float _delayAfterDeath;

    private void Awake() => GetComponent<Collider2D>().isTrigger = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) == false)
            return;

        Destroy(player.gameObject);

        StartCoroutine(RestartOnTimeout(_delayAfterDeath));
    }

    private IEnumerator RestartOnTimeout(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        _gameStateHandler.Restart();
    }
}
