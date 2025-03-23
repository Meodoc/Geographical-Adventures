using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonNavigationHandler : MonoBehaviour
{
    public Button defaultButton;

    private void Update()
    {
        // Check if the current selected button becomes non-interactive
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null)
        {
            Button currentButton = currentSelected.GetComponent<Button>();
            if (currentButton != null && (!currentButton.interactable || !currentButton.gameObject.activeInHierarchy))
            {
                SelectNextButton(currentButton);
            }
        }
    }

    private void SelectNextButton(Button currentButton)
    {
        Button nextButton = GetNextSelectable(currentButton, NavigationDirection.Right) ??
                            GetNextSelectable(currentButton, NavigationDirection.Down) ??
                            defaultButton;

        if (nextButton != null && nextButton.interactable)
        {
            EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        }
    }

    private Button GetNextSelectable(Button currentButton, NavigationDirection direction)
    {
        Selectable selectable = null;

        switch (direction)
        {
            case NavigationDirection.Left:
                selectable = currentButton.FindSelectableOnLeft();
                break;
            case NavigationDirection.Right:
                selectable = currentButton.FindSelectableOnRight();
                break;
            case NavigationDirection.Up:
                selectable = currentButton.FindSelectableOnUp();
                break;
            case NavigationDirection.Down:
                selectable = currentButton.FindSelectableOnDown();
                break;
        }

        // Check if the found selectable is a valid, interactable button
        if (selectable != null && selectable is Button button && button.interactable && button.gameObject.activeInHierarchy)
        {
            return button;
        }

        return null;
    }

    private enum NavigationDirection
    {
        Left,
        Right,
        Up,
        Down
    }
}