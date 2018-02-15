using System.Collections;
using UnityEngine;

public class GoToNextScene : MonoBehaviour {

    public void ChangeScene(string sceneName){
        Application.LoadLevel(sceneName);
    }
}
