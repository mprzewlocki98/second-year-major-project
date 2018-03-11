using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteMetalGame : MonoBehaviour {

	Animation animtion;
	bool played = false;
	bool showIcon = true;
	private AudioSource successSource; 

	void Start(){
		animtion = this.gameObject.GetComponent<Animation> ();
		successSource = this.gameObject.GetComponent<AudioSource> ();
		successSource.playOnAwake = false;
	}

	void Update(){
//		successDelegate methodToUse = checkSuccess;
		played = checkSuccess();

		if (played){
			if (showIcon) {
				animtion.Play ("wellDone");
				successSource.Play ();
				showIcon = false;
			}
			Invoke ("nextScene", 5);
		}
	}

	void nextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

//	delegate bool successDelegate();

	public bool checkSuccess(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("item");

		if (objs.Length == 0) {
			return true;
		}
		return false;
	}
}
