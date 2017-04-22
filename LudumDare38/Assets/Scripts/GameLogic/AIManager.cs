using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour 
{
	public static AIManager Instance = null;

	void Awake()
	{
		Instance = this;
	}

	private float nextAction = 0f;

	void Update () 
	{
		nextAction += Time.deltaTime;

		if(nextAction > 3f)
		{
			nextAction = 0f;

			GameplayManager.Instance.CreateMeeple(EMeepleTribe.Blue, new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)));
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
