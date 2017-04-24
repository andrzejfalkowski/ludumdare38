using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialArea : MonoBehaviour 
{
    public float BonusChargeValue = 0f;
	public float BonusAttackValue = 0f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        
        Meeple meeple = other.GetComponentInParent<Meeple>();
        if (meeple != null)
        {
            meeple.CurrentAreaBonus = BonusChargeValue;
			meeple.CurrentAttackBonus = BonusAttackValue;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        
        Meeple meeple = other.GetComponentInParent<Meeple>();
        if (meeple != null)
        {
            // Assuming there are no areas overlapping.
            meeple.CurrentAreaBonus = 0f;
			meeple.CurrentAttackBonus = 0f;
        }
    }
}
