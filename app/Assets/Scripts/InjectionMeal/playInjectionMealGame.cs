using UnityEngine;
using UnityEngine.SceneManagement;

public class playInjectionMealGame : MonoBehaviour {

    public string sceneName;

    /* Adding functionality to a button (or another component):
     *    
     *    attach the c# script to the button:
     *    [if it's a new script, first create it in project view/scripts
     *     then code; make sure the function is of public void type with 0/1 arguments]
     *    drag the script from project view to the desired button in hierarchy view.
     *    
     *    in inspector view in the on_click() functionality:
     *    select an object (new window pops, find the button in /scene/ option).
     *    select the script/method and optionally write an argument then ctrl_s
     */
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
