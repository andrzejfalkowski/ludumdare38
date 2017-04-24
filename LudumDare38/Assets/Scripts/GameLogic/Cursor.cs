using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Cursor : MonoBehaviour 
{
	[SerializeField]
	TextMeshProUGUI label;
    [SerializeField]
    Image icon;
	[SerializeField]
	Image hourglass;
	[SerializeField]
	Image resource;
    [SerializeField]
    GameObject allyRange;
    [SerializeField]
    Sprite iconWait;
    [SerializeField]
    Sprite iconPossible;
    [SerializeField]
    Sprite iconImpossible;
	[SerializeField]
	Sprite iconSword;
	[SerializeField]
	Sprite iconWater;

    bool emptySlot = false;
	bool spawnMode = true;
	bool overResource = false;
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

	void Start()
	{
		hourglass.transform.DOLocalRotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
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

		icon.color = Color.white;
        if (!SpawnMode)
		{
			label.text = "Engage spawn mode to place tribesmen";
		}
		else
        {
			hourglass.gameObject.SetActive(false);
            if (!emptySlot)
            {
                label.text = "Spot blocked";
                icon.sprite = iconImpossible;
				resource.gameObject.SetActive(false);
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

			if(GameplayManager.Instance.Player.MeepleCharge < 1f
				&& GameplayManager.Instance.Player.RemainingStartingMeeples <= 0)
			{
				label.text = "Breeding...";
				hourglass.gameObject.SetActive(true);
				if(icon.sprite == iconPossible)
					icon.color = Color.gray;
			}
		}

		if(emptySlot)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("SpecialAreas"));

			if(hit.collider != null && hit.transform.GetComponent<SpecialArea>() != null)
			{
				resource.gameObject.SetActive(true);
				if(hit.transform.GetComponent<SpecialArea>().BonusAttackValue > 0)
				{
					resource.sprite = iconSword;

					if(icon.sprite == iconPossible && !(GameplayManager.Instance.Player.MeepleCharge < 1f
						&& GameplayManager.Instance.Player.RemainingStartingMeeples <= 0))
					{
						icon.color = new Color(1f, 1f, 0.3f);
					}
						
				}
				else
				{
					resource.sprite = iconWater;

					if(icon.sprite == iconPossible && !(GameplayManager.Instance.Player.MeepleCharge < 1f
						&& GameplayManager.Instance.Player.RemainingStartingMeeples <= 0))
					{
						
						icon.color = new Color(0.5f, 1f, 1f);
					}
				}
			}
			else
			{
				resource.gameObject.SetActive(false);
			}
		}
		else
		{
			resource.gameObject.SetActive(false);
		}
	}
}
