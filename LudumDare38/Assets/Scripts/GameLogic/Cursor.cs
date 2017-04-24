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
    Image icon;
    [SerializeField]
    GameObject allyRange;
    [SerializeField]
    Sprite iconWait;
    [SerializeField]
    Sprite iconPossible;
    [SerializeField]
    Sprite iconImpossible;

    bool emptySlot = false;
	bool spawnMode = true;
    int alliesInRange = 0;

    public bool SpawnMode
	{
		get { return spawnMode; } set { spawnMode = value; }
	}

	public bool EmptySlot
	{
		get { return emptySlot; } set { emptySlot = value; }
    }

    public int AlliesInRange
    {
        get { return alliesInRange; }
        set { alliesInRange = value; }
    }

    void Update () 
	{
		if(GameplayManager.Instance == null)
			return;

		if(GameplayManager.Instance.GameOver)
			return;

		this.transform.position = Input.mousePosition;

		icon.gameObject.SetActive(SpawnMode);
        //allyRange.SetActive(SpawnMode);

        if (!SpawnMode)
		{
			label.text = "Engage spawn mode to place tribesmen";
		}
		else if(GameplayManager.Instance.Player.MeepleCharge < 1f
            && GameplayManager.Instance.Player.RemainingStartingMeeples <= 0)
		{
			label.text = "Breeding...";
            icon.sprite = iconWait;
		}
		else
        {
            if (!emptySlot)
            {
                label.text = "Spot blocked";
                icon.sprite = iconImpossible;
            }
            else if (alliesInRange == 0)
            {
                label.text = "Too far from the tribe";
                icon.sprite = iconImpossible;
            }
            else
			{
				label.text = "Place tribesman?";
                icon.sprite = iconPossible;
            }
		}
	}
}
