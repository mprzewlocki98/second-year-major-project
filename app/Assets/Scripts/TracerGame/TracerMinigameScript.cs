﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TracerMinigameScript : MonoBehaviour
{
    public Text scoreDisplay;
    public Animation animation;
    public Sprite glowing, notGlowing;

    private GameObject[] spots;
    private bool isGlowing;
    private int minigameScore = 0, spotsTapped = 0, max_spots = 10;

    public void Start()
    {
        spots = GameObject.FindGameObjectsWithTag("spot");

        setSpotRadius();

        isGlowing = false;
        setGlowingState();
        InvokeRepeating("setGlowingState", 1f, 1f);
    }

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

    // sets the spots collider radius based on game difficulty
    // a higher radius value means less accuracy is needed for a successful spot tap
    private void setSpotRadius()
    {
        bool easyMode = Difficulty.easyMode;
        float easyRadius = 0.6f, hardRadius = 0.4f;

        foreach (GameObject spot in spots)
        {
            CircleCollider2D collider = spot.GetComponent<CircleCollider2D>();
            if (easyMode) { collider.radius = easyRadius; }
            else { collider.radius = hardRadius; }
        }
    }

    // switch spot sprite between glowing and non-glowing
    private void setGlowingState()
    {
        foreach (GameObject spot in spots)
        {
            SpriteRenderer sprite = spot.GetComponent<SpriteRenderer>();

            // position and offset settings are a bugfix (glowing sprite incorrect defaults for them in unity)
            Transform position = spot.GetComponent<Transform>();
            Vector3 pos = new Vector3(0.35f, 2.55f, 0); 
            Vector2 offs = new Vector2(0.35f, 2.25f); 
            CircleCollider2D collider = spot.GetComponent<CircleCollider2D>();

            if (isGlowing)
            {
                sprite.sprite = glowing;

                position.position += pos;
                collider.offset -= offs;
            }
            else
            {
                sprite.sprite = notGlowing;

                position.position -= pos;
                collider.offset += offs;
            }
        }
        isGlowing = !isGlowing;
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