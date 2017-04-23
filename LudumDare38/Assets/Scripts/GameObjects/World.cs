using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour 
{

	void Update () 
	{
        UIManager.Instance.Cursor.AlliesInRange = GetAlliesNumber(GameplayManager.Instance.Player.Tribe);

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			return;

		int layerMask = (1 << 11);
		layerMask |= (1 << 12);
		layerMask |= (1 << LayerMask.NameToLayer("AllyRangeRed"));
		layerMask |= (1 << LayerMask.NameToLayer("AllyRangeBlue"));
        layerMask = ~layerMask;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

		if(hit.collider != null && hit.collider.transform == this.transform)
		{
			UIManager.Instance.Cursor.EmptySlot = true;
			if(Input.GetMouseButtonDown(0)
                && UIManager.Instance.Cursor.AlliesInRange > 0)
			{
				InputManager.Instance.WorldClicked();
			}
		}
		else
		{
			UIManager.Instance.Cursor.EmptySlot = false;
		}
    }

    private int GetAlliesNumber(EMeepleTribe tribe)
    {
        int layerMask = 1 << ((tribe == EMeepleTribe.Red) ?
            LayerMask.NameToLayer("AllyRangeRed") :
            LayerMask.NameToLayer("AllyRangeBlue"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit2D[] allies = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, layerMask);

        return allies.Length;
    }
}
