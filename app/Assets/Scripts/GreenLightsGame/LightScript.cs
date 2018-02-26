using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {

	public List<Sprite> lightImages;
    public Sprite greenLightImage;

    private GameControllerScript gcScript;

    // lights will be by default, inactive and not green
    public bool isActive = false;
	public bool isGreen = false;

	// This will be for initialisation
	void Start () {
		ResetPosition();
		gcScript = this.transform.parent.GetComponent<GameControllerScript>();
	}

	public void MakeGreen() {
		isGreen = true;
        this.GetComponent<SpriteRenderer>().sprite = greenLightImage;
    }
    
	public void MakeNotGreen() {
		isGreen = false;
		int lightChoice = Random.Range (0, lightImages.Count);
		this.GetComponent<SpriteRenderer>().sprite = lightImages[lightChoice];
	}
    
	void ResetPosition() {
        // the position that is outside of the screen
		this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
	}

	void Update() {
		if (this.transform.position.y > 6.0f && isActive) {
            // this is when the light has went out of the screen
			Deactivate();
		}
	}

	public void Activate() {
		isActive = true;
		float upSpeed = Random.Range (2.0f, 4.5f); // light speeds are random from 2 to 4.5
		this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, upSpeed, 0.0f);
		this.transform.position = new Vector3(Random.Range (-2.4f, 2.45f), -6.0f, 0.0f); 
        // this makes the lights look like they're moving by changing the position
        // the condition to either make the light green or otherwise
		if (Random.Range(0,4) == 0) {
			MakeGreen();
		} else {
			MakeNotGreen ();
		}
	}

	public void Deactivate() {
        // make the light disappear
		isActive = false;
		this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		ResetPosition();
	}

	void OnMouseDown() {
		Pop ();
	}

	public void Pop() {
		if (!isGreen) {
			Deactivate();
		} else {
			gcScript.AddPoints(1);
			Deactivate ();
		}
	}
}
