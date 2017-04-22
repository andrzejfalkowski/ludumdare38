using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponentInParent<Meeple>() != null)
		{
			other.GetComponentInParent<Meeple>().Fall();
		}
	}
}
