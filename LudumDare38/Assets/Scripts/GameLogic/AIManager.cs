﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour 
{
	public static AIManager Instance = null;

	public PlayerLogic SimulatedPlayer;

	[SerializeField]
	Collider2D worldCollider;

	void Awake()
	{
		Instance = this;

		SimulatedPlayer.OnChargeReady += SimulateCreatingMeeple;
	}

	void SimulateCreatingMeeple()
	{
		if(GameplayManager.Instance.GameOver)
			return;

		if(GameplayManager.Instance.Player.RemainingStartingMeeples > 0)
			return;

		int cnt = 0;
		// Get random point in collider
		Vector2 randomPoint = new Vector2(Random.Range(-7f, 34f), Random.Range(-12f, 7f));

		while((!worldCollider.OverlapPoint(randomPoint) || World.GetAlliesNumber(SimulatedPlayer.Tribe, randomPoint) == 0) && cnt < 5)
		{
			randomPoint = new Vector2(Random.Range(-7f, 34f), Random.Range(-12f, 7f));
			cnt++;
		}

		if(cnt < 5)
		{
			GameplayManager.Instance.CreateMeeple(SimulatedPlayer, randomPoint, 1);
			SimulatedPlayer.MeepleCharge = 0f;
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
