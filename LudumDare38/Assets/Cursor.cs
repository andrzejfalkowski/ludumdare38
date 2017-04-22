using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cursor : MonoBehaviour 
{
	[SerializeField]
	TextMeshProUGUI label;

	bool emptySlot = false;

	public bool EmptySlot
	{
		get { return emptySlot; } set { emptySlot = value; }
	}

	void Update () 
	{
		if(GameplayManager.Instance == null)
			return;

		this.transform.position = Input.mousePosition;

		if(GameplayManager.Instance.Player.MeepleCharge < 1f)
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
