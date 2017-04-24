﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour 
{
	public static GameplayManager Instance = null;

	public PlayerLogic Player;
	public PlayerLogic Opponent;

	public bool GameOver = false;

	void Awake()
	{
		Instance = this;
	}

	[SerializeField]
	Transform meeplesParent;
	[SerializeField]
	GameObject meeplePrefab;

	public List<Meeple> MeeplesOnMap;

	public void CreateMeeple(PlayerLogic player, Vector2 position, int alliesInRange)
	{
		GameObject meeple = GameObject.Instantiate(meeplePrefab);
		meeple.transform.SetParent(meeplesParent);
		meeple.transform.localScale = Vector3.one;
		meeple.transform.localPosition = position;

		meeple.GetComponent<Meeple>().Init(player, alliesInRange);
	}

	void Start()
	{
		UnityEngine.Cursor.visible = false;
	}

	void Update()
	{
		if(!GameOver)
		{
			if(Player.Population >= 100)
			{
				GameOver = true;
				UIManager.Instance.ShowVictoryScreen();
			}
			else if(Opponent.Population >= 100)
			{
				GameOver = true;
				UIManager.Instance.ShowDefeatScreen();
			}
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
