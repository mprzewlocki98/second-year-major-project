using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	public State currentState = State.OPEN_CREAM;

	void Start () {
		
	}
		
	void Update () {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton (0)) {
			RaycastHit2D[] touches = Physics2D.RaycastAll (pos, pos, 0.5f);

			foreach(RaycastHit2D r in touches){
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

	void Done () {
		Debug.Log ("All done!");
	}
}
