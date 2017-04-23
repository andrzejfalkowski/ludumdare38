using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour 
{

	void Update () 
	{
		if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			return;

		if(GameplayManager.Instance.GameOver)
			return;

		int layerMask = (1 << 11);
		layerMask |= (1 << 12);
		layerMask = ~layerMask;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

		if(hit.collider != null && hit.collider.transform == this.transform)
		{
			UIManager.Instance.Cursor.EmptySlot = true;
			if(Input.GetMouseButtonDown(0))
			{
				InputManager.Instance.WorldClicked();
			}
		}
		else
		{
			UIManager.Instance.Cursor.EmptySlot = false;
		}
	}
}
