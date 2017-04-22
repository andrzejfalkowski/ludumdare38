using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
	public static InputManager Instance = null;

	void Awake()
	{
		Instance = this;
	}

	public void WorldClicked()
	{
		if(GameplayManager.Instance.Player.MeepleCharge >= 1f)
		{
			GameplayManager.Instance.Player.MeepleCharge = 0f;
			GameplayManager.Instance.CreateMeeple(EMeepleTribe.Red, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
