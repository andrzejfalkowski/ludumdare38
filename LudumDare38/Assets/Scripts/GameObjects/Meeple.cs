﻿using System.Collections;
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
	SpriteRenderer spriteRenderer;

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
				spriteRenderer.color = Color.red;
				physicsBlock.layer = LayerMask.NameToLayer("MeepleRed");
				shockwave.layer = LayerMask.NameToLayer("MeepleRedShockwave");
				break;
			case EMeepleTribe.Blue:
				spriteRenderer.color = Color.blue;
				physicsBlock.layer = LayerMask.NameToLayer("MeepleBlue");
				shockwave.layer = LayerMask.NameToLayer("MeepleBlueShockwave");
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

	public void Fall()
	{
		this.GetComponent<Rigidbody2D>().gravityScale = 1f;

		DOVirtual.DelayedCall(3f, () => { Destroy(this.gameObject); }, false);
	}

	void OnDestroy()
	{
		if(GameplayManager.Instance != null && GameplayManager.Instance.MeeplesOnMap.Contains(this))
			GameplayManager.Instance.MeeplesOnMap.Remove(this);
	}
}
