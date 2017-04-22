using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour 
{
	[SerializeField]
	private bool upperVoid = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponentInParent<Meeple>() != null)
		{
			other.GetComponentInParent<Meeple>().Fall(upperVoid);
		}
	}
}
