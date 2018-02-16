using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;	
using UnityEngine;

public class DragHandler :MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPostion;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPostion = transform.position;
	}

	#endregion


	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
//		transform.position = Input.mousePosition;

	}

	#endregion


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		transform.position = startPostion;
	}

	#endregion
}
