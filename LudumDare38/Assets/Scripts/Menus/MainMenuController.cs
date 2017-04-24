using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField]
	GameObject HowToPlayPopup;

	[SerializeField]
	Texture2D cursor;

	void Start()
	{
		UnityEngine.Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	public void NewGameClicked()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void HowToPlayClicked()
	{
		HowToPlayPopup.SetActive(true);
	}

	public void HowToPlayBackClicked()
	{
		HowToPlayPopup.SetActive(false);
	}

	public void ExitClicked()
	{
		Application.Quit();
	}
}
