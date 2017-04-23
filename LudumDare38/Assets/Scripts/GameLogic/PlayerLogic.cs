using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour 
{
	public EMeepleTribe Tribe;

	public float MeepleCharge = 0f;
	public float MeepleChargeSpeedBase = 0.1f;
    private float meepleChargeSpeedBonus = 0f;
    private float meepleChargeSpeed = 0f;

    public int Population = 0;

	public Action OnChargeReady = null;

	void Update()
	{
		MeepleCharge = MeepleCharge + Time.deltaTime * meepleChargeSpeed;

		if(MeepleCharge > 1f)
		{
			MeepleCharge = 1f;

			if(OnChargeReady != null)
			{
				OnChargeReady();
			}
		}
	}

    private void LateUpdate()
    {
        // Using late update to make sure all bonuses were reported.
        meepleChargeSpeed = MeepleChargeSpeedBase + meepleChargeSpeedBonus;
        meepleChargeSpeedBonus = 0;
    }

    public void AddChargeBonus(float bonus)
    {
        meepleChargeSpeedBonus += bonus;
    }
}
