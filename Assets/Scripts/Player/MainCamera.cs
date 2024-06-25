using UnityEngine;
using Zenject;

public class MainCamera : MonoBehaviour
{
    [Inject] private Player target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        var smoothedPosition = Vector3.Lerp(transform.position, target.transform.position + offset, smoothSpeed);
        transform.position = smoothedPosition;
    }

}