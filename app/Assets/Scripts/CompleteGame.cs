using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteGame : MonoBehaviour {

	Animation animtion;
	bool played = false;
	bool showIcon = true;

	void Start(){
		animtion = GetComponent<Animation> ();
	}

	void Update(){
		successDelegate methodToUse = checkSuccess;
		played = methodToUse.Invoke();

		if (played){
			if (showIcon) {
				animtion.Play ("wellDone");
				showIcon = false;
			}
			Invoke ("nextScene", 5);
		}
	}

	void nextScene(){
		SceneManager.LoadScene ("NextScene");
	}

	delegate bool successDelegate();

	bool checkSuccess(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("item");

		if (objs.Length == 0) {
			return true;
		}
		return false;
	}
}
