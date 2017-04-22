using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour 
{
	public static AIManager Instance = null;

	public PlayerLogic SimulatedPlayer;

	void Awake()
	{
		Instance = this;

		SimulatedPlayer.OnChargeReady += SimulateCreatingMeeple;
	}

	void SimulateCreatingMeeple()
	{
		GameplayManager.Instance.CreateMeeple(SimulatedPlayer.Tribe, new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)));
		SimulatedPlayer.MeepleCharge = 0f;
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
