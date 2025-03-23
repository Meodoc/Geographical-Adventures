using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineOnSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Outline outline = gameObject.GetComponent<Outline>();
        outline.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Outline outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
}