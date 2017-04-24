using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour 
{
	[SerializeField]
	Transform meepleParent;

	void Update () 
	{
        UIManager.Instance.Cursor.AlliesInRange = GetAlliesNumber(GameplayManager.Instance.Player.Tribe);

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			return;

		if(GameplayManager.Instance.GameOver)
			return;

		int layerMask = (1 << 11);
		layerMask |= (1 << 12);
		layerMask |= (1 << LayerMask.NameToLayer("AllyRangeRed"));
		layerMask |= (1 << LayerMask.NameToLayer("AllyRangeBlue"));
		layerMask |= (1 << LayerMask.NameToLayer("SpecialAreas"));
        layerMask = ~layerMask;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

		if(hit.collider != null && hit.collider.transform == this.transform
            && !hit.collider.isTrigger
            && hit.collider.gameObject.GetComponentInParent<Meeple>() == null)
		{
			UIManager.Instance.Cursor.EmptySlot = true;
            if (Input.GetMouseButtonDown(0)
                && UIManager.Instance.Cursor.AlliesInRange > 0)
			{
				InputManager.Instance.WorldClicked(UIManager.Instance.Cursor.AlliesInRange);
			}
		}
		else
		{
			UIManager.Instance.Cursor.EmptySlot = false;
		}
    }

    public static int GetAlliesNumber(EMeepleTribe tribe)
    {
        int layerMask = 1 << ((tribe == EMeepleTribe.Red) ?
            LayerMask.NameToLayer("AllyRangeRed") :
            LayerMask.NameToLayer("AllyRangeBlue"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit2D[] allies = Physics2D.GetRayIntersectionAll(ray, 10f, layerMask);

        return allies.Length;
    }

	public static int GetAlliesNumber(EMeepleTribe tribe, Vector2 pos)
	{
		int layerMask = 1 << ((tribe == EMeepleTribe.Red) ?
			LayerMask.NameToLayer("AllyRangeRed") :
			LayerMask.NameToLayer("AllyRangeBlue"));

		RaycastHit2D[] allies = Physics2D.RaycastAll(pos, Vector2.zero, 10f, layerMask);

//		RaycastHit2D hit = new RaycastHit2D();
//		//RaycastHit2D(
//		//Ray ray =   Camera.main.ScreenPointToRay(pos);
//		Ray ray = Physics2D.Raycast(pos, Vector2.zero,
		//RaycastHit2D[] allies = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, layerMask);

		return allies.Length;
	}
}
