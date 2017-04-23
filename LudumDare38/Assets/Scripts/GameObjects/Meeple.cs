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
	[HideInInspector]
	public EMeepleTribe Tribe;

	[SerializeField]
	SpriteRenderer[] spriteRenderers;

	[SerializeField]
	GameObject shockwave;
	[SerializeField]
	GameObject physicsBlock;

	public void Init(EMeepleTribe newTribe)
	{
		Tribe = newTribe;

		switch(newTribe)
		{
		case EMeepleTribe.Red:
                foreach (SpriteRenderer sprite in spriteRenderers)
                {
                    sprite.color = Color.red;
                }
				physicsBlock.layer = LayerMask.NameToLayer("MeepleRed");
				shockwave.layer = LayerMask.NameToLayer("MeepleRedShockwave");
				GameplayManager.Instance.Player.Population++;
				break;
			case EMeepleTribe.Blue:
                transform.localScale = new Vector3(-1, 1, 1);
                foreach (SpriteRenderer sprite in spriteRenderers)
                {
                    sprite.color = Color.blue;
                }
                physicsBlock.layer = LayerMask.NameToLayer("MeepleBlue");
				shockwave.layer = LayerMask.NameToLayer("MeepleBlueShockwave");
				GameplayManager.Instance.Opponent.Population++;
				break;
		}

		GameplayManager.Instance.MeeplesOnMap.Add(this);

		StartShockwave();
	}

	void StartShockwave()
	{
		this.GetComponent<Rigidbody2D>().mass = 10f;

		Color startColor = shockwave.GetComponent<SpriteRenderer>().color;

		shockwave.transform.DOScale(new Vector3(8f, 8f, 1f), 1f);
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

	public void Fall(bool upperVoid)
	{
		

		Collider2D[] colliders = this.GetComponentsInChildren<Collider2D>();
		for(int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = false;
		}

		this.GetComponent<Rigidbody2D>().gravityScale = 1f;
		DOVirtual.DelayedCall(3f, () => { Destroy(this.gameObject); }, false);

		if(upperVoid)
        {
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.sortingOrder = -100;
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
}
