using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour {

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene("11-cutscene");
    }
}
