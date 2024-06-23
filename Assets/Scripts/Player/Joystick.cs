using DependencyInjection;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDependencyProvider, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Provide] private Joystick ProvideJoystick() => this;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    private Vector2 inputVector;
    private Vector2 touchPosition;
    private float handleRadius;

    public Vector2 InputVector => inputVector;

    private void Start() => handleRadius = background.sizeDelta.y;

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeVisibility(true);

        touchPosition = eventData.position;
        handle.transform.position = touchPosition;
        background.transform.position = touchPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;

        ChangeVisibility(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var dragPosition = eventData.position;
        inputVector = (dragPosition - touchPosition).normalized;

        var distance = Vector2.Distance(dragPosition, touchPosition);

        if (distance < handleRadius)
        {
            handle.transform.position = touchPosition + inputVector * distance;
            return;
        }

        handle.transform.position = touchPosition + inputVector * handleRadius;
    }

    private void ChangeVisibility(bool isVisible)
    {
        handle.gameObject.SetActive(isVisible);
        background.gameObject.SetActive(isVisible);
    }

}