using System.Collections;
using UnityEngine;

public class Loop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bowl bowl) == false)
            return;
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
