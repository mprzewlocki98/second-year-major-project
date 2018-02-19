using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public Transform lightPrefab;
	public Text scoreDisplay;

	private int playerScore = 0;
	private float timeSinceLastSpawn = 0.0f;
	private float timeToSpawn = 0.0f;
	private List<Transform> lights;

	private const int LIGHTS_POOL = 20;

	void Start () {
		lights = new List<Transform>();
		for (int i = 0; i < LIGHTS_POOL; i++) {
			Transform oneLight = Instantiate(lightPrefab) as Transform;
			oneLight.parent = this.transform;
			lights.Add(oneLight);
		}
		SpawnLights();
		GameStart();
	}

    // Update is called once per frame
    void Update () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= timeToSpawn) {
			SpawnLights();
		}
	}

	void SpawnLights() {
		timeSinceLastSpawn = 0.0f;
		timeToSpawn = Random.Range (0.0f, 2.0f);
		foreach (Transform b in lights) {
			LightScript ls = b.GetComponent<LightScript>();
			if (ls && !ls.isActive) {
				ls.Activate();
				break;
			}
		}
	}

	public void AddPoints(int points=1) {
        playerScore += points;
        UpdateScoreDisplay();
	}

	public void GameOver() {
		SavePoints();
	}

	void UpdateScoreDisplay() {
        scoreDisplay.text = "Score:  " + playerScore.ToString();
	}

	public void GameStart() {
		UpdateScoreDisplay();
	}

	void SavePoints() {
		PlayerPrefs.SetInt("Points", playerScore);
	}
}
