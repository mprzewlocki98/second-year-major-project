using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteLightGame : MonoBehaviour {

	Animation anim;
    GameControllerScript gc;
	bool played = false;
	bool showIcon = true;

	void Start(){
		anim = GetComponent<Animation> ();
        gc = GetComponent<GameControllerScript>();
	}

	void Update(){
        successDelegate methodToUse = checkSuccess;
        played = methodToUse.Invoke();

        if (played)
        {
            if (showIcon)
            {
                gc.setSprender(true);
                anim.Play("wellDone");
                showIcon = false;
            }
            Invoke("PlayerWon", 1);
        }
    }

    void PlayerWon()
    {
        SceneManager.LoadScene("4-cutscene");
    }


    delegate bool successDelegate();

    bool checkSuccess()
    {
        if (gc.getPlayerScore() == 1)
        {
            return true;
        }
        return false;
    }
}
