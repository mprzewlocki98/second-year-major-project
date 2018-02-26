using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InjectionScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	public State currentState = State.OPEN_CREAM;
    private bool easyMode = false;
	private bool draggingItem = false;
	private GameObject draggedObject, syringe1, syringe2, lid, creamBlob;
    private Animation syringeAnimation, lidAnimation, creamBlobAnimation, wellDoneAnimation;
	private Collider2D veinCollider;
	private Vector3 lastGoodPosition;

    // execute at start of game
	void Start () {
        // load all the game objects so that they can be used
		syringe1 = GameObject.Find ("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        lid = GameObject.Find("CreamLid");
        creamBlob = GameObject.Find("CreamBlob");

        veinCollider = GameObject.Find("Vein").GetComponent<Collider2D>();

        syringeAnimation = syringe2.GetComponent<Animation>();
        lidAnimation = lid.GetComponent<Animation>();
        creamBlobAnimation = creamBlob.GetComponent<Animation>();
        wellDoneAnimation = GameObject.Find("wellDone").GetComponent<Animation>();

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
					draggingItem = true;
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

    // delay disabling of an object
    private IEnumerator DisableObject(GameObject obj) {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    // delay setting of state
    private IEnumerator SetState(State targetState, int time = 1) {
        yield return new WaitForSeconds(time);
        currentState = targetState;
    }

    // drops an item at the current position
	private void DropItem() {
        if (draggedObject != null && draggingItem)
            if (draggedObject == syringe1) {
                if (currentState == State.MOVE_SYRINGE && syringe1.GetComponent<Collider2D>().IsTouching(veinCollider))
                {
                    lastGoodPosition = syringe1.transform.position; // the last position of the syringe that made sense in the state
                    StartCoroutine(SetState(State.INJECT_SYRINGE));
                } else {
                    StartCoroutine(TweenMovement(syringe1, syringe1.transform.position, lastGoodPosition));
                }
            }
            if (draggedObject == creamBlob) {
                if(currentState == State.APPLY_CREAM && creamBlob.GetComponent<Collider2D>().IsTouching(veinCollider)) {
                    creamBlobAnimation.Play();
                    StartCoroutine(DisableObject(creamBlob));
                    StartCoroutine(SetState(State.MOVE_SYRINGE));
            } else {
                    StartCoroutine(TweenMovement(creamBlob, creamBlob.transform.position, creamBlob.transform.position + new Vector3(0, -10, 0)));
                }
            }

		draggingItem = false;
	}

    // change the state of the game based on the object that was clicked
    private IEnumerator ChangeState(RaycastHit2D raycastHit){
       switch (currentState) {
            case State.OPEN_CREAM:
                if (raycastHit.collider.name == "CreamLid") {
                    lidAnimation.Play();
                    StartCoroutine(SetState(State.APPLY_CREAM));
                }

                break;
            case State.APPLY_CREAM:
                if (raycastHit.collider.name == "Cream") {
                    draggingItem = true;
                    draggedObject = creamBlob;
                }

                break;
            case State.INJECT_SYRINGE:
                if (raycastHit.collider.name == "Syringe2") {
                    syringeAnimation.Play();
                    currentState = State.DONE;
                    StartCoroutine(Done());
                }

                break;
       }

        yield return new WaitForSeconds(1);
    }
		
	void Update () {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton (0)) {
			RaycastHit2D[] touches = Physics2D.RaycastAll (pos, pos, 0.5f); // all the touch positions in raw format

            if (touches.Length > 0) {
                foreach (RaycastHit2D r in touches) {
                    StartCoroutine(ChangeState(r));
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

    // executed when the game is done
	private IEnumerator Done() {
		Debug.Log ("All done!");
        yield return new WaitForSeconds(1);
        wellDoneAnimation.Play();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("8-cutscene");
	}
}
