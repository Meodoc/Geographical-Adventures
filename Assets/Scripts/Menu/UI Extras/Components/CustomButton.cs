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


	private void SetLabel(string text)
	{
		if (label)
		{
			label.text = text;
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
		
		// Use a coroutine to update the label after the UI update
		StartCoroutine(UpdateLabelAfterFrame());
    }

	private IEnumerator UpdateLabelAfterFrame()
	{
		yield return new WaitForEndOfFrame();
		SetLabel($"<   {localizer.currentValue}   >");
	}
	
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		onPointerExit?.Invoke();
	}

	public override void OnDeselect(BaseEventData eventData)
    {
		base.OnDeselect(eventData);
		SetLabel(localizer.currentValue);
    }
}
