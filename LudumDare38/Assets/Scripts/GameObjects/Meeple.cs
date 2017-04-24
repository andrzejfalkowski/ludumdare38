using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EMeepleTribe
{
	Red,
	Blue
}

public class Meeple : MonoBehaviour 
{
	[SerializeField]
	public bool autoInit;

	public EMeepleTribe Tribe;

	[SerializeField]
	SpriteRenderer[] colorfulSpriteRenderers;
	[SerializeField]
	Color redColor;
	[SerializeField]
	Color blueColor;

	[SerializeField]
	SpriteRenderer[] spriteRenderers;

	[SerializeField]
	SpriteRenderer attackIcon;
	[SerializeField]
	SpriteRenderer spawnIcon;

	[SerializeField]
	GameObject shockwave;
    [SerializeField]
    GameObject physicsBlock;
    [SerializeField]
    GameObject allyRange;

    private PlayerLogic owner;
    private float currentAreaBonus = 0f;
    public float CurrentAreaBonus
    {
        get { return currentAreaBonus; }
        set { currentAreaBonus = value; }
    }

	private float currentAttackBonus = 0f;
	public float CurrentAttackBonus
	{
		get { return currentAttackBonus; }
		set { currentAttackBonus = value; }
	}

	void Start()
	{
		if(autoInit)
		{
			Init(Tribe == EMeepleTribe.Red ? GameplayManager.Instance.Player : GameplayManager.Instance.Opponent, 0);
		}
	}

    private void Update()
    {
        if (owner != null)
        {
            owner.AddChargeBonus(GetOccupiedLandValue());
			owner.AddAttackBonus(GetAttackValue());
        }
    }

    public void Init(PlayerLogic owner, int alliesInRange)
	{
        this.owner = owner;
		Tribe = owner.Tribe;

		switch(owner.Tribe)
		{
		case EMeepleTribe.Red:
				transform.localScale = new Vector3(-1, 1, 1);
				foreach (SpriteRenderer sprite in colorfulSpriteRenderers)
                {
					sprite.color = redColor;
                }
				physicsBlock.layer = LayerMask.NameToLayer("MeepleRed");
				shockwave.layer = LayerMask.NameToLayer("MeepleRedShockwave");
                allyRange.layer = LayerMask.NameToLayer("AllyRangeRed");
				GameplayManager.Instance.Player.Population++;
				break;
			case EMeepleTribe.Blue:
				foreach (SpriteRenderer sprite in colorfulSpriteRenderers)
                {
					sprite.color = blueColor;
                }
                physicsBlock.layer = LayerMask.NameToLayer("MeepleBlue");
				shockwave.layer = LayerMask.NameToLayer("MeepleBlueShockwave");
                allyRange.layer = LayerMask.NameToLayer("AllyRangeBlue");
				GameplayManager.Instance.Opponent.Population++;
                break;
		}
		UIManager.Instance.PunchPopulation(owner.Tribe);

		GameplayManager.Instance.MeeplesOnMap.Add(this);

		StartShockwave(alliesInRange);
	}

	void StartShockwave(int alliesInRange)
	{
		this.GetComponent<Rigidbody2D>().mass = 1000f;

		Color startColor = shockwave.GetComponent<SpriteRenderer>().color;

		shockwave.transform.DOScale(new Vector3(6f, 4f, 1f) * GetAlliesModifier(alliesInRange) * owner.MeepleAttack, 1f);
		DOTween.To(
			() => shockwave.GetComponent<SpriteRenderer>().color.a, 
			(a) => shockwave.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, a), 0f, 1f)
			.OnComplete(
				() => 
				{
					shockwave.gameObject.SetActive(false);
					this.GetComponent<Rigidbody2D>().mass = 1f;
				}
			);
	}
    
    private float GetAlliesModifier(int alliesInRange)
    {
        // Value between 1 - 3;
        return 1f + 2f *((float)alliesInRange - 1f) / ((float)alliesInRange + 4f);
    }

	public void Fall(bool upperVoid, int spriteOrder)
	{
		DynamicZ[] dynamicZz = this.GetComponentsInChildren<DynamicZ>();
		for(int i = 0; i < dynamicZz.Length; i++)
		{
			dynamicZz[i].enabled = false;
		}

		Collider2D[] colliders = this.GetComponentsInChildren<Collider2D>();
		for(int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = false;
		}

		this.GetComponent<Rigidbody2D>().gravityScale = 1f;
		DOVirtual.DelayedCall(3f, () => { Destroy(this.gameObject); }, false);

		if(upperVoid)
		{
			foreach(SpriteRenderer sprite in spriteRenderers)
			{
				sprite.sortingOrder = spriteOrder;
			}
		}
	}

	void OnDestroy()
	{
		if(GameplayManager.Instance != null && GameplayManager.Instance.MeeplesOnMap.Contains(this))
		{
			GameplayManager.Instance.MeeplesOnMap.Remove(this);

			switch(Tribe)
			{
			case EMeepleTribe.Red:
				GameplayManager.Instance.Player.Population--;
				break;
			case EMeepleTribe.Blue:
				GameplayManager.Instance.Opponent.Population--;
				break;
			}
		}
	}

	private float GetAttackValue()
	{
		attackIcon.gameObject.SetActive(currentAttackBonus > 0f);

		return currentAttackBonus;
	}

    private float GetOccupiedLandValue()
    {
		spawnIcon.gameObject.SetActive(currentAreaBonus > 0f);

        return 0.02f + currentAreaBonus;
    }
}
