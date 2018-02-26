using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour {

    private GameObject button;
    private ButtonClick buttonScript;
	
	void Start () {
        
        button = GameObject.Find("button");
        buttonScript = button.GetComponent<ButtonClick>();
	}
	
	void Update () {
		
        if (buttonScript.GetGameComplete()) {

            SpriteRenderer SR = gameObject.GetComponent<SpriteRenderer>();
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 255f);

            Invoke("NextScene", 3);
        }

	}

    void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
