﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteLightGame : MonoBehaviour {

	Animation anim;
    GameControllerScript gc;
	bool played = false;
	bool showIcon = true;

	void Start(){
        // linking the variables to the components
		anim = GetComponent<Animation> ();
        gc = GetComponent<GameControllerScript>();
	}

	void Update(){
        // this will always check if player has won the game
        successDelegate methodToUse = checkSuccess;
        played = methodToUse.Invoke();

        if (played) {
            if (showIcon) {
                // makes the sprite renderer visible
                gc.setSprender(true);
                // play the animation
                anim.Play("wellDone");
                // stop the animation after once played
                showIcon = false;
            }

            // Invoke allows a certain waiting time
            Invoke("PlayerWon", 1);
        }
    }

    void PlayerWon() {
        SceneManager.LoadScene("4-cutscene");
    }

    delegate bool successDelegate();

    bool checkSuccess() {
        if (gc.getPlayerScore() == 10) {
            return true;
        }
        return false;
    }
}
