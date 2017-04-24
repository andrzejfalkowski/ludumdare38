using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicZ : MonoBehaviour 
{
	private SpriteRenderer spriteRenderer;

	public bool OnlyOnStart = true;

	[SerializeField]
	List<SpriteRenderer> spriteRenderers;
	[SerializeField]
	List<int> additionalZs;
	[SerializeField]
	int shiftZ = 0;
	[SerializeField]
	int additionalZ = 0;

	void Start()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		if(OnlyOnStart)
		{
			RefreshZ();
			this.enabled = false;
		}
	}

	void Update () 
	{
		if(!OnlyOnStart)
			RefreshZ();
	}

	void RefreshZ()
	{
		int sortingOrder = (-Mathf.RoundToInt((this.transform.position.y) * 10) + shiftZ) * 10 + additionalZ;

		for(int i = 0; i < spriteRenderers.Count; i++)
		{
			spriteRenderers[i].sortingOrder = sortingOrder + additionalZs[i];
		}
	}
}
