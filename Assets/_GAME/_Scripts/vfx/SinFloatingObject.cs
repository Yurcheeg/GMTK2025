using UnityEngine;

public class SinFloatingObject : MonoBehaviour
{
    [SerializeField] private float _maxOffset;

    private void Update() => transform.localPosition = new(0, Mathf.Sin(transform.position.x + Time.time * 1.5f) * _maxOffset);
}
