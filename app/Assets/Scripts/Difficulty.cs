using UnityEngine;

public class Difficulty : MonoBehaviour {

    public static bool easyMode = true;

    public void SetEasyMode() {

        easyMode = true;
    }

    public void SetHardMode() {

        easyMode = false;
    }

}
