using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteMetalGame : MonoBehaviour {

	private bool played = false;
	private bool showIcon = true;
	private Animation animation;
	private AudioSource successSource; 

	// Use this for initialization
	void Start(){
		animation = this.gameObject.GetComponent<Animation> ();
		successSource = this.gameObject.GetComponent<AudioSource> ();
		successSource.playOnAwake = false;
	}


	// Update is called once per frame
	void Update(){
		played = CompleteMetalGame.checkSuccess();

		if (played){
			if (showIcon) {
				animation.Play ("wellDone");
				successSource.Play ();
				showIcon = false;
			}
			Invoke ("nextScene", 2);
		}
	}


	// move to the next scene
	private void nextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


	// check whether the game is success or not
	public static bool checkSuccess(){
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("item");

		if (objs.Length == 0) {
			return true;
		}
		return false;
	}
}
