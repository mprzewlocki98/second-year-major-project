using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTextController : MonoBehaviour {

	// Use this for initialization
	private Text hint;
	private int process = 0;
	private int numberOfItem = 6;

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
					process = 3;
				}
			}
		} else {
			if (process == 1) {
				Invoke ("cleanText", 3);
				process = 3;
			}
		}

		if (GameObject.Find ("wellDone").GetComponent<CompleteMetalGame> ().checkSuccess()) {
			process = 3;
			hint.text = "Congratulation !!!";
		}
	}

	void cleanText(){
		hint.text = "";
	}

	void initText(){
		process = 1;
	}

	int numberOfItemLeft(){
		return GameObject.FindGameObjectsWithTag ("item").Length;
	}
}
