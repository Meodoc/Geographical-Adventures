using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GeoGame.Localization;

public class CustomButton : Button
{

	public event System.Action onPointerEnter;
	public event System.Action onPointerExit;

	public StringLocalizer localizer;
	[Header("Settings")]
	//public string buttonText;
	public bool changeTextOnMouseOver;
	//public string mouseOverButtonText;

	[Header("References")]
	public TMPro.TMP_Text label;


	void SetLabel(string text)
	{
		if (label)
		{
			label.text = text;
			// Debug.Log("Label text set: " + text + ", Actual label text: " + label.text);
			label.ForceMeshUpdate(true);
		}
	}


	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(this.gameObject);
		onPointerEnter?.Invoke();
	}

	public override void OnSelect(BaseEventData eventData)
    {
		base.OnSelect(eventData);
        // Debug.Log(this.gameObject.name + " was selected");
		if (changeTextOnMouseOver)
		{
			SetLabel($"<   {localizer.currentValue}   >");
		}
    }

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		// EventSystem.current.SetSelectedGameObject(null);
		onPointerExit?.Invoke();
	}

	public override void OnDeselect(BaseEventData eventData)
    {
		base.OnDeselect(eventData);
		SetLabel(localizer.currentValue);
    }
}
