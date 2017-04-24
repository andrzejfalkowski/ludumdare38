using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalManager : MonoBehaviour 
{
	public static ImmortalManager Instance = null;

	public float SpeedUp = 0f;

	void Awake () 
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
	}
		
	void OnDestroy () 
	{
		if(Instance == this)
			Instance = null;
	}
}
