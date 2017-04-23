using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	void Update()
	{
		playerChargeBar.fillAmount = GameplayManager.Instance.Player.MeepleCharge;
		opponentChargeBar.fillAmount = GameplayManager.Instance.Opponent.MeepleCharge;

		playerPopulation.text = GameplayManager.Instance.Player.Population.ToString();
		opponentPopulation.text = GameplayManager.Instance.Opponent.Population.ToString();
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
