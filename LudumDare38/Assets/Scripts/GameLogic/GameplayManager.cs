using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour 
{
	public static GameplayManager Instance = null;

	public PlayerLogic Player;
	public PlayerLogic Opponent;

	void Awake()
	{
		Instance = this;
	}

	[SerializeField]
	Transform meeplesParent;
	[SerializeField]
	GameObject meeplePrefab;

	public List<Meeple> MeeplesOnMap;

	public void CreateMeeple(EMeepleTribe tribe, Vector2 position)
	{
		GameObject meeple = GameObject.Instantiate(meeplePrefab);
		meeple.transform.SetParent(meeplesParent);
		meeple.transform.localScale = Vector3.one;
		meeple.transform.localPosition = position;

		meeple.GetComponent<Meeple>().Init(tribe);
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
