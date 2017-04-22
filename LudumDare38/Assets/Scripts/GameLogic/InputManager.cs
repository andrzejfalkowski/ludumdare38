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
		GameplayManager.Instance.CreateMeeple(EMeepleTribe.Red, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
