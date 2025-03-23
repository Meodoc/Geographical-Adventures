using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{

	public Button quitButton;

	void Start()
	{
		quitButton.onClick.AddListener(GameController.ExitToMainMenu);
	}


	public void TogglePauseMenu()
	{
		if (IsOpen)
		{
			CloseMenu();
		}
		else
		{
			OpenMenu();
		}
	}


	protected override void OnMenuOpened()
	{
		SetSelectedGameObject(buttonFirstSelected);
		
		GameController.SetPauseState(true);
	}

	protected override void OnMenuClosed()
	{
		SetSelectedGameObject(mainMenuButtonFirstSelected);
		
		GameController.SetPauseState(false);
	}

}
