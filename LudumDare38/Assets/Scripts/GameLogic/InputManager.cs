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

	public void WorldClicked(int alliesInRange)
	{
		if(UIManager.Instance.Cursor.SpawnMode && UIManager.Instance.Cursor.EmptySlot && GameplayManager.Instance.Player.MeepleCharge >= 1f)
		{
			GameplayManager.Instance.Player.MeepleCharge = 0f;
			GameplayManager.Instance.CreateMeeple(
                GameplayManager.Instance.Player,
                (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition),
                alliesInRange);
		}
	}

	public void SwitchSpawnMode(bool value)
	{
		UIManager.Instance.Cursor.SpawnMode = value;
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
