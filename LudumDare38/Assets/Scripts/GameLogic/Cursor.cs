using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cursor : MonoBehaviour 
{
	[SerializeField]
	TextMeshProUGUI label;
	[SerializeField]
	GameObject tribesmanSymbol;

	bool emptySlot = false;
	bool spawnMode = false;

	public bool SpawnMode
	{
		get { return spawnMode; } set { spawnMode = value; }
	}

	public bool EmptySlot
	{
		get { return emptySlot; } set { emptySlot = value; }
	}

	void Update () 
	{
		if(GameplayManager.Instance == null)
			return;

		this.transform.position = Input.mousePosition;

		tribesmanSymbol.SetActive(SpawnMode);

		if(!SpawnMode)
		{
			label.text = "Engage spawn mode to place tribesmen";
		}
		else if(GameplayManager.Instance.Player.MeepleCharge < 1f)
		{
			label.text = "Breeding...";
		}
		else
		{
			if(emptySlot)
			{
				label.text = "Place tribesman?";
			}
			else
			{
				label.text = "Spot blocked";
			}
		}
	}
}
