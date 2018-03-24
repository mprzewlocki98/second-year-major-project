using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject choiceCanvas;

	public void NextScene(int page) {

        SceneManager.LoadScene(page);
    }

    public void SetActiveChoiceCanvas() {

        choiceCanvas.SetActive(true);
    }

    public void SetNotActiveChoiceCanvas()
    {
        choiceCanvas.SetActive(false);
    }
}
