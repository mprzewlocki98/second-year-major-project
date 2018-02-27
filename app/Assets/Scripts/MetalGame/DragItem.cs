using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour {
	float decaySecond = 1f;
	float elaspedSecond = 0;
	float distance = 10;
	bool flag = false;
	GameObject obj;

	void start (){
		obj = GetComponent<GameObject> ();
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
	}

	void OnMouseUp () {
		flag = true;
		Destroy(this.gameObject,decaySecond); //delete
	}

	void Update(){
		if (flag) {
			float scaleRate = (decaySecond - elaspedSecond)/decaySecond;
			transform.localScale *= scaleRate;
			elaspedSecond += Time.deltaTime;
		}
			
	}
}
