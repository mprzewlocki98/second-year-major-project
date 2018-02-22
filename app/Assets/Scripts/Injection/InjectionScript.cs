using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	public State currentState = State.OPEN_CREAM;
    private bool easyMode = false;
	private bool draggingItem = false;
	private GameObject draggedObject, syringe1, syringe2, lid, creamBlob;
    private Animation syringeAnimation, lidAnimation;
	private Collider2D veinCollider;
	private Vector3 lastGoodPosition;

	void Start () {
        // load all the game objects so that they can be used
		syringe1 = GameObject.Find ("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        lid = GameObject.Find("CreamLid");
        creamBlob = GameObject.Find("CreamBlob");
        veinCollider = GameObject.Find("Vein").GetComponent<Collider2D>();
        syringeAnimation = syringe2.GetComponent<Animation>();
        lidAnimation = lid.GetComponent<Animation>();
        lastGoodPosition = syringe1.transform.position;
    }

	// Code based on http://unity.grogansoft.com/drag-and-drop/

    // check for whether there is any input
	private bool HasInput {
		get { return Input.GetMouseButton (0); }
	}

    // return the current position of input
	private Vector2 CurrentTouchPosition {
		get { return Camera.main.ScreenToWorldPoint (Input.mousePosition); }
	}

    // drags or picks up a game object at the current touch position
	private void DragOrPickup() {
		var inputPosition = CurrentTouchPosition; 
		if (draggingItem) {
			draggedObject.transform.position = inputPosition;
		} else {
			RaycastHit2D[] touches = Physics2D.RaycastAll (inputPosition, inputPosition, 0.5f);
			if (touches.Length > 0) {
				var hit = touches [0];
				if (hit.transform != null && !hit.collider.isTrigger) { // we only want to pick up some of the items
					draggingItem = true;;
					draggedObject = hit.transform.gameObject;
				}
			}
		}
	}

    // smoothly moves an object from one place to another, used for moving the syringe back to its original position
    private IEnumerator TweenMovement(GameObject obj, Vector3 currentPosition, Vector3 targetPosition){
        var FRAMES = 30.0f;
        for(var t = 0; t <= FRAMES; t++){
            obj.transform.SetPositionAndRotation(Vector3.Lerp(currentPosition, targetPosition, (t / FRAMES)), obj.transform.rotation);
            yield return new WaitForEndOfFrame();
        }
    }

    // make a blob of cream to be used for the second state
    private void CreateCreamBlob(){
        GameObject creamBlobClone = Instantiate(creamBlob);

    }

    // drops an item at the current position
	private void DropItem() {
		if(draggedObject != null && draggedObject == syringe1 && draggingItem){
			if (currentState == State.MOVE_SYRINGE && draggedObject.GetComponent<Collider2D>().IsTouching(veinCollider)) {
				lastGoodPosition = draggedObject.transform.position; // the last position of the syringe that made sense in the state
				currentState = State.INJECT_SYRINGE;
			} else {
                StartCoroutine(TweenMovement(draggedObject, draggedObject.transform.position, lastGoodPosition));
			}
		}

		draggingItem = false;
	}
		
	void Update () {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton (0)) {
			RaycastHit2D[] touches = Physics2D.RaycastAll (pos, pos, 0.5f); // all the touch positions in raw format

            if (touches.Length > 0) {
                foreach (RaycastHit2D r in touches)
                {
                    switch (currentState) // game behaviour varies based on the current state
                    {
                        case State.OPEN_CREAM:
                            if (r.collider.name == "CreamLid")
                            {
                                lidAnimation.Play();
                                currentState = State.APPLY_CREAM;
                            }
                            break;
                        case State.APPLY_CREAM:
                            if (r.collider.name == "Cream")
                            {
                                Debug.Log("Correct!");
                                currentState = State.MOVE_SYRINGE;
                            }
                            break;
                        case State.MOVE_SYRINGE:
                            break;
                        case State.INJECT_SYRINGE:
                            if (r.collider.name == "Syringe2")
                            {
                                syringeAnimation.Play();
                                Debug.Log("Correct!");
                                currentState = State.DONE;
                                Done();
                            }
                            break;
                    }
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
	}

	void Done () {
		Debug.Log ("All done!");
	}
}
