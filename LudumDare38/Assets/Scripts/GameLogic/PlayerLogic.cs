using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour 
{
	public EMeepleTribe Tribe;

    [System.NonSerialized]
	public float MeepleCharge = 1f;
	public float MeepleChargeSpeedBase = 0.1f;
    private float meepleChargeSpeedBonus = 0f;
    private float meepleChargeSpeed = 0f;

	[System.NonSerialized]
	public float MeepleAttack = 1f;
	public float MeepleAttackBase = 1f;
	private float meepleAttackBonus = 0f;

    [SerializeField]
    private int startingMeeples = 5;
    public int RemainingStartingMeeples
    {
        get { return startingMeeples; }
    }

    public int Population = 0;

	public Action OnChargeReady = null;

	void Update()
	{
		float mod = 1f;
		if(Tribe == EMeepleTribe.Blue)
			mod += ImmortalManager.Instance.SpeedUp;

		MeepleCharge = MeepleCharge + Time.deltaTime * meepleChargeSpeed * mod;

		if(MeepleCharge > 1f)
		{
			MeepleCharge = 1f;
            if (startingMeeples <= 0)
            {
                AvailableMeeplesController.Instance.SetIcons(1);
            }

            if (OnChargeReady != null)
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

		MeepleAttack = MeepleAttackBase + meepleAttackBonus;
		meepleAttackBonus = 0;
    }

    public void AddChargeBonus(float bonus)
    {
        meepleChargeSpeedBonus += bonus;
    }

	public void AddAttackBonus(float bonus)
	{
		meepleAttackBonus += bonus;
	}

    public void TakeStartingMeeple()
    {
        startingMeeples--;
        AvailableMeeplesController.Instance.SetIcons(startingMeeples);
        if (startingMeeples <= 0)
        {
            MeepleCharge = 0f;
        }
    }
}
