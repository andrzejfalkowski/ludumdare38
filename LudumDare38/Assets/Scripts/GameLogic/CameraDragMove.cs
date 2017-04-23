using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using DG.Tweening;

public class CameraDragMove : MonoBehaviour 
{
	public static bool BlockDrag = false;

	private Vector3 resetCamera;
	private Vector3 origin;
	private Vector3 difference;
	public bool Drag = false;

	private Vector3 target;
	private bool snap = false;

	public const float MAX_ZOOM = 5f;
	public const float DEFAULT_ZOOM_IN = 4f;
	public const float DEFAULT_ZOOM_OUT = 8f;
	public const float MIN_ZOOM = 15f;
	public const float ZOOM_SPEED = 0.5f;

	public const float ARROWS_MOVE_SPEED = 3f;

	void Start () 
	{
		resetCamera = Camera.main.transform.position;
	}

	void LateUpdate() 
	{
		if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
			return;

		if(GameplayManager.Instance.GameOver)
			return;

		if(snap)
		{
			return;
		}
			
		Vector3 viewportMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		if(Input.GetMouseButton(1) && !BlockDrag) 
		{
			difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
			if(!Drag)
			{
				Drag = true;
				origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		} 
		else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || 
			Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
			viewportMousePosition.x < 0.05f || viewportMousePosition.x > 0.95f ||
			viewportMousePosition.y < 0.05f || viewportMousePosition.y > 0.95f)
		{
			//Debug.Log(Input.mousePosition);

			origin = this.transform.position;
			DOTween.Kill("DragCamera");
			Drag = false;

			difference = Vector3.zero;

			if(Input.GetKey(KeyCode.LeftArrow) || viewportMousePosition.x < 0.05f)
				difference.x -= ARROWS_MOVE_SPEED;
			else if(Input.GetKey(KeyCode.RightArrow) || viewportMousePosition.x > 0.95f)
				difference.x += ARROWS_MOVE_SPEED;

			if(Input.GetKey(KeyCode.UpArrow) || viewportMousePosition.y > 0.95f)
				difference.y += ARROWS_MOVE_SPEED;
			else if(Input.GetKey(KeyCode.DownArrow) || viewportMousePosition.y < 0.05f)
				difference.y -= ARROWS_MOVE_SPEED;

			Vector3 target = origin + difference;
			target.x = Mathf.Min(Mathf.Max(-10f, target.x), 40f);
			target.y = Mathf.Min(Mathf.Max(-15f, target.y), 10f);

			this.transform.DOMove(target, 0.3f).SetId("DragCamera").SetUpdate(UpdateType.Late);
		}
		else 
		{
			Drag = false;
		}

		if(Drag)
		{
			//Camera.main.transform.position = origin - difference;
			DOTween.Kill("DragCamera");
			this.transform.DOMove(origin - difference, 0.3f).SetId("DragCamera").SetUpdate(UpdateType.Late);
		}

		if(Mathf.Abs(Input.mouseScrollDelta.y) > 0f) 
		{
			//Camera.main.orthographicSize = 
			float targetSize = 
				Mathf.Clamp(Camera.main.orthographicSize - (Input.mouseScrollDelta.y * ZOOM_SPEED), MAX_ZOOM, MIN_ZOOM);
			
			DOTween.To(()=>Camera.main.orthographicSize, (size)=>{Camera.main.orthographicSize = size;}, targetSize, 0.05f)
				.SetUpdate(UpdateType.Late);
		}
	}

	public void SnapTo(Vector3 snapTarget, bool zoom = true, float time = 0.3f, Action callback = null)
	{
		target = new Vector3(snapTarget.x, snapTarget.y, resetCamera.z);
		snap = true;
		this.transform.DOMove(target, time)
			.OnComplete(
			()=>
			{
				snap = false;
				if(callback != null)
					callback();
			})
			.SetUpdate(UpdateType.Late);
		
		if(zoom && Camera.main.orthographicSize > DEFAULT_ZOOM_IN)
		{
			ZoomIn();
		}
	}

	public void ZoomIn(float time = 0.3f)
	{
		DOTween.To(()=>Camera.main.orthographicSize, (x)=>{Camera.main.orthographicSize = x;}, DEFAULT_ZOOM_IN, time)
			.SetUpdate(UpdateType.Late);
	}

	public void ZoomOut(float time = 0.3f)
	{
		DOTween.To(()=>Camera.main.orthographicSize, (x)=>{Camera.main.orthographicSize = x;}, DEFAULT_ZOOM_OUT, time)
			.SetUpdate(UpdateType.Late);
	}
} 
