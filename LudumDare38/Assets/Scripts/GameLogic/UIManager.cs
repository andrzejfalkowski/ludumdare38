using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

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

	public void Update()
	{
		if(GameplayManager.Instance.GameOver)
			return;

		playerChargeBar.fillAmount = GameplayManager.Instance.Player.MeepleCharge;
		opponentChargeBar.fillAmount = GameplayManager.Instance.Opponent.MeepleCharge;

		playerPopulation.text = GameplayManager.Instance.Player.Population.ToString();
		opponentPopulation.text = GameplayManager.Instance.Opponent.Population.ToString();
	}

	public void PunchPopulation(EMeepleTribe tribe)
	{
		if(tribe == EMeepleTribe.Red)
		{
			DOTween.Kill("punchPlayerPopulation");
			playerPopulation.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1f), 0.5f).SetId("punchPlayerPopulation");
		}
		else if(tribe == EMeepleTribe.Red)
		{
			DOTween.Kill("punchOpponentPopulation");
			opponentPopulation.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1f), 0.5f).SetId("punchOpponentPopulation");
		}
	}

	public void ShowVictoryScreen()
	{
		victoryScreen.SetActive(true);
	}

	public void ShowDefeatScreen()
	{
		defeatScreen.SetActive(true);
	}


	public void TryAgainHarderClicked()
	{
		ImmortalManager.Instance.SpeedUp += 0.1f;
		SceneManager.LoadScene("Gameplay");
	}

	public void TryAgainClicked()
	{
		SceneManager.LoadScene("Gameplay");
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
