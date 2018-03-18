﻿using UnityEngine;

public class PlayMetalGame : MonoBehaviour {
	
	private float hintSecond = 1f;
	private float speed = 10f;	//how fast item shakes
	private float amount = 2f;	//how many times item shakes
	private bool processEnd = true;
	private Vector3 mousePos;

    private bool easyMode = Difficulty.easyMode;


	// Use this for initialization
	void Start () {
		hintSecond = Time.time;
		mousePos = new Vector3 (0,0,0);
	}
		
	// Update is called once per frame
	void Update () {
		if (Difficulty.easyMode) {
			
			GameObject obj = GameObject.FindWithTag ("item");

			if (obj != null && Time.time - hintSecond >= 8) {
			
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
				mousePos = Input.mousePosition;
			}
		}
	}
}