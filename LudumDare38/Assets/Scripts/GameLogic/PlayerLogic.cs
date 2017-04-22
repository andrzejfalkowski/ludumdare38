using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour 
{
	public EMeepleTribe Tribe;

	public float MeepleCharge = 0f;
	public float MeepleChargeSpeed = 0.1f;

	public Action OnChargeReady = null;

	void Update()
	{
		MeepleCharge = MeepleCharge + Time.deltaTime * MeepleChargeSpeed;

		if(MeepleCharge > 1f)
		{
			MeepleCharge = 1f;

			if(OnChargeReady != null)
			{
				OnChargeReady();
			}
		}
	}
}
