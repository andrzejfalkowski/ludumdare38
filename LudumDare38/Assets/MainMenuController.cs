using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	[SerializeField]
	GameObject HowToPlayPopup;

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
