using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour {
	float hintSecond = 1f;
	float speed = 10f; //how fast it shakes
	float amount = 2f;//how much it shakes
	bool processEnd = true;
	Vector3 mousePos;

	// Use this for initialization
	void Start () {
		hintSecond = Time.time;
		mousePos = new Vector3 (0,0,0);
	}
		
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.FindWithTag ("item");

		if (obj != null && Time.time - hintSecond >= 5) {
			
			SpriteRenderer SR = obj.GetComponent<SpriteRenderer> ();

			if (amount > 0) {
				processEnd = false;
				SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, Mathf.Sin (Time.time * speed));
				amount -= Time.deltaTime;

			} else {

				SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, 1);
				amount = 2f;
				hintSecond = Time.time;
				processEnd = true;
			}	
		}
			
		if (Input.mousePosition != mousePos && processEnd) {
			amount = 2f;
			hintSecond = Time.time;
//			Debug.Log ("mouse click");
			mousePos = Input.mousePosition;
		}
	}
}



//				obj.transform.localScale = new Vector3 (scale);
//
//				if (obj.transform.localScale.x < 0.9f) {
//					obj.transform.localScale.Set(0.9f,0.9f,0);
//					scale = 1.1f;
//					scale += Time.deltaTime * 0.1f;
//				} else if (obj.transform.localScale.x > 1.1f) {
//					obj.transform.localScale.Set(1.1f,1.1f,0);
//					scale = 0.9f;
//					scale += Time.deltaTime * 0.1f;
//				} else {
//					scale += Time.deltaTime * 0.1f;
//				}


//				obj.transform.Rotate (Mathf.Sin (Time.time * speed),Mathf.Sin (Time.time * speed), 0);
//				Vector3 v1 = new Vector3 (25, 25, 0);
//				obj.transform.Rotate(v1*Time.deltaTime);
//				Vector3 v2 = new Vector3 (25, -25, 0);
//				obj.transform.Rotate(v2*Time.deltaTime);