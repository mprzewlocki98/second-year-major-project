using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	public State currentState = State.OPEN_CREAM;
	private bool draggingItem = false;
	private GameObject draggedObject, syringe1;
	private Collider2D veinCollider;

	void Start () {
		syringe1 = GameObject.Find ("Syringe1");
		veinCollider = GameObject.Find("Vein").GetComponent<Collider2D>();
	}

	// Code based on http://unity.grogansoft.com/drag-and-drop/

	private bool HasInput {
		get { return Input.GetMouseButton (0); }
	}

	private Vector2 CurrentTouchPosition {
		get { return Camera.main.ScreenToWorldPoint (Input.mousePosition); }
	}

	private void DragOrPickup() {
		var inputPosition = CurrentTouchPosition; 
		if (draggingItem) {
			draggedObject.transform.position = inputPosition;
		} else {
			RaycastHit2D[] touches = Physics2D.RaycastAll (inputPosition, inputPosition, 0.5f);
			if (touches.Length > 0) {
				var hit = touches [0];
				if (hit.transform != null && !hit.collider.isTrigger) {
					draggingItem = true;;
					draggedObject = hit.transform.gameObject;
				}
			}
		}
	}

	private void DropItem() {
		if(draggedObject != null && draggedObject == syringe1 && draggingItem){
			if (draggedObject.GetComponent<Collider2D>().IsTouching(veinCollider)) {
				Debug.Log ("Syringe Dragged Correctly");
				} else {
				
			}
		}

		draggingItem = false;
	}
		
	void Update () {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton (0)) {
			RaycastHit2D[] touches = Physics2D.RaycastAll (pos, pos, 0.5f);

			foreach (RaycastHit2D r in touches) {
				switch (currentState) {
				case State.OPEN_CREAM: 
					if (r.collider.name == "CreamLid") {
						Debug.Log ("Correct!");
						currentState = State.APPLY_CREAM;
					}
					break;
				case State.APPLY_CREAM:
					if (r.collider.name == "Cream") {
						Debug.Log ("Correct!");
						currentState = State.MOVE_SYRINGE;
					}
					break;
				case State.MOVE_SYRINGE:
					if (r.collider.name == "Syringe1") {
						Debug.Log ("Correct!");
						currentState = State.INJECT_SYRINGE;
					}
					break;
				case State.INJECT_SYRINGE:
					if (r.collider.name == "Syringe2") {
						Debug.Log ("Correct!");
						currentState = State.DONE;
						Done ();
					}
					break;
				}
			}
		}
	}

	void FixedUpdate () {
		if (Input.GetMouseButton (0)) {
			DragOrPickup (); 
		} else {
			DropItem ();
		}

		if(syringe1.GetComponent<Collider2D>().IsTouching(veinCollider)){
			Debug.Log ("The syringe is touching the vein");
		}
	}

	void Done () {
		Debug.Log ("All done!");
	}
}
