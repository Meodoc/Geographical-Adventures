using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Player player;
	public GameObject game;
	public PauseMenu pauseMenu;
	public GameObject gameUI;
	public GameObject[] hideInMapView;
	public MapMenu map;
	public Compass compass;
	public GameObject gamepadMapSelector;
	public CanvasGroup hudGroup;
	public Toggle invertInputToggle;
	public PlayerInputHandler playerInputHandler;

	public float smoothT;
	float smoothV;


	void Awake()
	{
		hudGroup.alpha = 0;
	}

	void Update()
	{
		bool uiIsActive = GameController.IsState(GameState.Playing) || GameController.IsState(GameState.ViewingMap);

		hudGroup.alpha = Mathf.SmoothDamp(hudGroup.alpha, uiIsActive ? 1 : 0, ref smoothV, smoothT);


	}

	public void ToggleMap()
	{
		if (GameController.IsAnyState(GameState.Playing, GameState.ViewingMap))
		{
			ToggleMapDisplay();
		}
	}

	public void TogglePause()
	{
		if (GameController.IsAnyState(GameState.Playing, GameState.ViewingMap, GameState.Paused))
		{
			pauseMenu.TogglePauseMenu();
		}
	}


	public void ToggleMapDisplay()
	{
		bool showMap = map.ToggleActive(player);
		if (showMap)
		{
			GameController.SetState(GameState.ViewingMap);
			gamepadMapSelector.SetActive(playerInputHandler.InGamepadState);
		}
		else
		{
			GameController.SetState(GameState.Playing);
			gamepadMapSelector.SetActive(false);
		}

		Seb.Helpers.GameObjectHelper.SetActiveAll(!showMap, hideInMapView);
	}


	public void ToggleGamepadMapSelector()
	{
		if (GameController.IsState(GameState.ViewingMap))
		{
			gamepadMapSelector.SetActive(playerInputHandler.InGamepadState);
		}
	}
}
