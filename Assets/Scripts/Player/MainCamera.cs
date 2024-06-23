using DependencyInjection;
using UnityEngine;

public class MainCamera : MonoBehaviour, IDependencyProvider
{
    [Provide] private MainCamera ProvideCamera() => this;
    [Inject] private Player target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        var smoothedPosition = Vector3.Lerp(transform.position, target.transform.position + offset, smoothSpeed);
        transform.position = smoothedPosition;
    }

}