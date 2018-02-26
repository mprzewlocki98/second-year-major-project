using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instruction : MonoBehaviour {

    public void PlayGame(int page)
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + page);
    }

}
