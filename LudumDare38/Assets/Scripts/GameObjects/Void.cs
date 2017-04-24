using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour 
{
	[SerializeField]
	private bool upperVoid = false;
	[SerializeField]
	private int spriteOrder = 0;

	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter", other.gameObject);
        if (other.isTrigger)
        {
            Debug.Log("trigger");
        }
        else
        {
            Debug.Log("not trigger");
        }
        if (GameplayManager.Instance.GameOver)
			return;

		if(other.GetComponentInParent<Meeple>() != null
            && !other.isTrigger)
		{
			other.GetComponentInParent<Meeple>().Fall(upperVoid, spriteOrder);
		}
	}
}
