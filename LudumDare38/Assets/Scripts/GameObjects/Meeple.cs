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
	SpriteRenderer spriteRenderer;
	[SerializeField]
	GameObject shockwave;

	public void Init(EMeepleTribe newTribe)
	{
		Tribe = newTribe;

		switch(newTribe)
		{
			case EMeepleTribe.Red:
				spriteRenderer.color = Color.red;
				break;
			case EMeepleTribe.Blue:
				spriteRenderer.color = Color.blue;
				break;
		}

		GameplayManager.Instance.MeeplesOnMap.Add(this);

		StartShockwave();
	}

	void StartShockwave()
	{
		Color startColor = shockwave.GetComponent<SpriteRenderer>().color;

		shockwave.transform.DOScale(new Vector3(3f, 3f, 1f), 3f);
		DOTween.To(
			() => shockwave.GetComponent<SpriteRenderer>().color.a, 
			(a) => shockwave.GetComponent<SpriteRenderer>().color = new Color(startColor.r, startColor.g, startColor.b, a),
			0f,
			3f);
	}

	void OnDestroy()
	{
		if(GameplayManager.Instance != null && GameplayManager.Instance.MeeplesOnMap.Contains(this))
			GameplayManager.Instance.MeeplesOnMap.Remove(this);
	}
}
