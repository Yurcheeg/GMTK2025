using UnityEngine;
/// <summary>
/// Temporary script to test the functionality inside a build
/// Attached to main camera
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed;
    void LateUpdate()
    {
        Vector2 desiredPosition = _target.position;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position,desiredPosition, _speed);
        transform.position = new Vector3(smoothedPosition.x,smoothedPosition.y,transform.position.z);

        transform.LookAt(_target);
    }
}
