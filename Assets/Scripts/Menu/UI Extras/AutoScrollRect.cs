using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class AutoScrollRect : MonoBehaviour
{
    // Sets the speed to move the scrollbar
    public float scrollSpeed = 10f;

    // Set as Template Object via (Your Dropdown Button > Template)
    public ScrollRect templateScrollRect;

    // Set as Template Viewport Object via (Your Dropdown Button > Template > Viewport)
    public RectTransform templateViewportTransform;

    // Set as Template Content Object via (Your Dropdown Button > Template > Viewport > Content)
    public RectTransform contentRectTransform;

    public PlayerInputHandler playerInputHandler;

    private RectTransform _selectedRectTransform;

    void Update()
    {
        if (playerInputHandler.InGamepadState)
        {
            UpdateScrollToSelected(templateScrollRect, contentRectTransform, templateViewportTransform);
        }
    }

    void UpdateScrollToSelected(
        ScrollRect scrollRect,
        RectTransform contentRectTransform,
        RectTransform viewportRectTransform)
    {
        // Get the current selected option from the eventsystem.
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        
        if (selected == null)
        {
            return;
        }

        if (selected.transform.parent.parent != contentRectTransform.transform)
        {
            return;
        }

        _selectedRectTransform = selected.transform.parent.GetComponent<RectTransform>();

        // Math stuff
        Vector3 selectedDifference = viewportRectTransform.localPosition - _selectedRectTransform.localPosition;
        float contentHeightDifference = (contentRectTransform.rect.height - viewportRectTransform.rect.height);

        float selectedPosition = (contentRectTransform.rect.height - selectedDifference.y);
        float currentScrollRectPosition = scrollRect.normalizedPosition.y * contentHeightDifference;
        float above = currentScrollRectPosition - (_selectedRectTransform.rect.height / 2) +
                      viewportRectTransform.rect.height / 2;
        float below = currentScrollRectPosition + (_selectedRectTransform.rect.height / 2) -
                      viewportRectTransform.rect.height / 2;

        // Check if selected option is out of bounds.
        if (selectedPosition > above)
        {
            float step = selectedPosition - above;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0, newNormalizedY),
                scrollSpeed * Time.deltaTime);
        }
        else if (selectedPosition < below)
        {
            float step = selectedPosition - below;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0, newNormalizedY),
                scrollSpeed * Time.deltaTime);
        }
    }
}