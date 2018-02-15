using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour {

    public string sceneName;
    // total score keeps track of the overall score of the player
    private int totalScore = 0;
    // minigame score is the current minigame's score; when completed is added to the total score then is reset to 0
    private int minigameScore = 0;
    // tracer minigame specific variables
    //TODO @ Emma: move these within a separate area with restricted access
    private int tracerSpotsTapped = 0, tracerSpotsTotal = 4;

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

    /* Checking whether a gameObject was clicked (can be reused in other scenarios with minimum change)
     * 
     * Code for tracer minigame, made with mouse clicks rather than taps for testing purposes
     * When the user clicks on the screen it checks if it clicked the required spots and
     * prints appropriate message to the console.
     */
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
           
            //TODO @ Emma: increase tag area for younger age category
            if (hitInfo && hitInfo.transform.gameObject.tag == "spot")
            {
                Debug.Log(hitInfo.transform.gameObject.name);
                Debug.Log("Spot tapped!");
                minigameScore ++;
                tracerSpotsTapped++;
                if (tracerSpotsTapped == 4)
                {
                    Debug.Log("Tracer Minigame score: " + minigameScore);
                    // add minigame score to total player's score
                    if (minigameScore > 0)
                    {
                        totalScore += minigameScore;
                    }
                    // reset minigame score for next minigame
                    minigameScore = 0;
                    // load next scene
                    LoadScene("gameplay3 dog");
                }
            }
            else
            {
                // tapping outside the target spots areas lossed the player a point
                //TODO @ Emma: only older age category should lose points
                Debug.Log("Missed!");
                minigameScore--;
            }
        }
    }

}
