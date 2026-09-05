using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class FloatingOnScreenStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickContainer;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private OnScreenStick onScreenStick;

    private Canvas canvas;
    private Camera uiCamera;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            uiCamera = canvas.worldCamera;
        }

        SetVisibility(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            eventData.position,
            uiCamera,
            out Vector2 localPoint
        );

        joystickContainer.anchoredPosition = localPoint;
        SetVisibility(true);

        // Forward down event
        if (onScreenStick != null)
        {
            onScreenStick.OnPointerDown(eventData);
        }
    }

    // --- NEW: Forward the drag event to OnScreenStick ---
    public void OnDrag(PointerEventData eventData)
    {
        if (onScreenStick != null)
        {
            onScreenStick.OnDrag(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // --- NEW: Forward the up event so OnScreenStick resets its handle & input ---
        if (onScreenStick != null)
        {
            onScreenStick.OnPointerUp(eventData);
        }

        SetVisibility(false);
    }

    private void SetVisibility(bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1f : 0f;
        // Keep blocksRaycasts FALSE so it does not intercept or block pointer drag events
        canvasGroup.blocksRaycasts = false;
    }
}