﻿using System.Collections;
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
    ResultsController results;

	bool allowGameOver = false;


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

	public void ShowResults(EGameOverResult result, EGameOverType type)
	{
        results.Show(result, type);
		DOVirtual.DelayedCall(1.5f, () => { allowGameOver = true; });
	}


	public void TryAgainHarderClicked()
	{
		if(allowGameOver)
		{
			ImmortalManager.Instance.SpeedUp += 0.2f;
			SceneManager.LoadScene("Gameplay");
		}
	}

	public void TryAgainClicked()
	{
		if(allowGameOver)
			SceneManager.LoadScene("Gameplay");
	}

	public void BackToMainMenuClicked()
	{
		if(allowGameOver)
			SceneManager.LoadScene("MainMenu");
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
