using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextController : MonoBehaviour {

	private Text hint;
	private int process = 0;	// 0 is inital state, 1 is playing state, 2 is complete state
	private int numberOfItem = 6;

	// Use this for initialization
	void Start () {
		hint = this.gameObject.GetComponent<Text>();
		hint.text = "Remove the metal items!";	
	}
	
	// Update is called once per frame
	void Update () {
		if (process == 0) {
			Invoke ("initText",3);
		}
		if (Difficulty.easyMode) {
			if (process == 1) {
				int temp = numberOfItemLeft ();
				if (temp != numberOfItem) {
					if (temp > 1) {
						hint.text = temp + " items left!";
					} else {
						hint.text = temp + " item left!";
					}

					numberOfItem = temp;
				}
				if (temp == 0) {
					process = 2;
				}
			}
		} else {
			if (process == 1) {
				
				if (Difficulty.easyMode) {
					Invoke ("cleanText", 4);
				}

				process = 2;
			}
		}

		if (CompleteMetalGame.checkSuccess()) {
			process = 2;
			hint.text = "Congratulations !!!";
		}
	}

	// clean the hint text
	private void cleanText(){
		hint.text = "";
	}

	// process from 0 to 1
	private void initText(){
		process = 1;
	}

	// to check how many items left
	private int numberOfItemLeft(){
		return GameObject.FindGameObjectsWithTag ("item").Length;
	}
}
