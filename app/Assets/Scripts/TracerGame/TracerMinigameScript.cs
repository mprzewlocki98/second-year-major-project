using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TracerMinigameScript : MonoBehaviour
{
    public Text scoreDisplay;
    public Animation animation;

    private int minigameScore = 0, spotsTapped = 0, max_spots = 10;
    private bool easyMode = Difficulty.easyMode;

    // Checking whether a gameObject was clicked 
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            
            if (hitInfo && hitInfo.transform.gameObject.tag == "spot")
            {
                SpotTapped();
                // deactivate spot, so it can't be tapped again
                hitInfo.transform.gameObject.active = false;
            }
            else
            {
                SpotMissed();
            }
            UpdateScoreDisplay();
        }
    }

    // handles the situation if the user has tapped one of the targed areas
    public void SpotTapped()
    {
        Debug.Log("Spot tapped!");
        minigameScore++;
        spotsTapped++;
        if (spotsTapped == max_spots)
        {
            GameWon();
        }
    }

    // handles the situation if the user has tapped outside the targed areas
    private void SpotMissed()
    {
        // tapping outside the target spots areas lossed the player a point
        Debug.Log("Missed!");
        if (minigameScore > 0)
        {
            minigameScore--;
        }
    }

    private void GameWon()
    {
        animation.Play("wellDone");
        Debug.Log("Game won! Final game score: " + minigameScore);
        StartCoroutine(Wait(3));
    }

    // wait then load scene; needed to show wellDone animation before proceeding
    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        LoadScene("10-cutscene");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = "Score: " + minigameScore.ToString();
    }

}