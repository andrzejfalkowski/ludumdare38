using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour 
{
	public static UIManager Instance = null;

	void Awake()
	{
		Instance = this;
	}

	public Cursor Cursor;

	[SerializeField]
	Image playerChargeBar;
	[SerializeField]
	Image opponentChargeBar;

	[SerializeField]
	TextMeshProUGUI playerPopulation;
	[SerializeField]
	TextMeshProUGUI opponentPopulation;

	[SerializeField]
	GameObject victoryScreen;
	[SerializeField]
	GameObject defeatScreen;

	void Update()
	{
		if(GameplayManager.Instance.GameOver)
			return;

		playerChargeBar.fillAmount = GameplayManager.Instance.Player.MeepleCharge;
		opponentChargeBar.fillAmount = GameplayManager.Instance.Opponent.MeepleCharge;

		playerPopulation.text = GameplayManager.Instance.Player.Population.ToString();
		opponentPopulation.text = GameplayManager.Instance.Opponent.Population.ToString();
	}

	public void ShowVictoryScreen()
	{
		victoryScreen.SetActive(true);
	}

	public void ShowDefeatScreen()
	{
		defeatScreen.SetActive(true);
	}

	public void BackToMainMenuClicked()
	{
		SceneManager.LoadScene("MainMenu");
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
