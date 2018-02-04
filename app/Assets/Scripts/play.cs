using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour {

    public string sceneName;

    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
