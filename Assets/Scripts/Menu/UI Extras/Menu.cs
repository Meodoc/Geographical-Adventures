using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
	public event System.Action menuClosedEvent;

	public GameObject menuHolder;
	public Button closeButton;
	public SubMenu[] subMenus;
	public GameObject firstButton;
	public GameObject mainMenuButton;
	public GameObject pauseMenuButton;


	protected virtual void Awake()
	{
		if (closeButton != null)
		{
			closeButton.onClick.AddListener(CloseMenu);
		}
		if (subMenus != null)
		{
			foreach (var submenu in subMenus)
			{
				submenu.openButton.onClick.AddListener(submenu.menu.OpenMenu);
				submenu.openButton.onClick.AddListener(OnSubMenuOpened);
				submenu.menu.menuClosedEvent += OnSubMenuClosed;
				// If the menu is closed while one of its submenus is open, the submenu should be closed as well
				menuClosedEvent += submenu.menu.CloseMenu;
			}
		}

		// If open at start, then trigger OnOpened so any needed setup code can be run
		if (IsOpen)
		{
			OnMenuOpened();
		}
	}

	[NaughtyAttributes.Button()]
	public void OpenMenu()
	{
		if (!IsOpen)
		{
			menuHolder.SetActive(true);
			OnMenuOpened();
		}
	}
	[NaughtyAttributes.Button()]
	public void CloseMenu()
	{
		if (IsOpen)
		{
			menuClosedEvent?.Invoke();
			menuHolder.SetActive(false);
			OnMenuClosed();
		}
	}

	protected virtual void OnMenuOpened()
	{
		if (firstButton)
		{
//			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(firstButton);
			Debug.Log("OnMenuOpened() called and selected gameObject set to: " + EventSystem.current.currentSelectedGameObject);
		}

	}

	protected virtual void OnMenuClosed()
	{
		if (mainMenuButton && GameController.IsState(GameState.InMainMenu))
		{
			EventSystem.current.SetSelectedGameObject(mainMenuButton);
		} else if (pauseMenuButton && GameController.IsState(GameState.Paused))
		{
			EventSystem.current.SetSelectedGameObject(pauseMenuButton);
		}
	}

	protected virtual void OnSubMenuOpened()
	{

	}

	protected virtual void OnSubMenuClosed()
	{
		// Debug.Log("Submenu closed: " + this.gameObject.name);
	}

	public bool IsOpen
	{
		get
		{
			return menuHolder.activeSelf;
		}
	}

	[System.Serializable]
	public struct SubMenu
	{
		public Menu menu;
		public Button openButton;
	}
}
