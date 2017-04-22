using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	void Update()
	{
		playerChargeBar.fillAmount = GameplayManager.Instance.Player.MeepleCharge;
		opponentChargeBar.fillAmount = GameplayManager.Instance.Opponent.MeepleCharge;
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
