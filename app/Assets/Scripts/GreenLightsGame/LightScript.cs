using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {

	public List<Sprite> lightImages;
    public Sprite greenLightImage;

    private GameControllerScript gcScript;

    public bool isActive = false;
	public bool isGreen = false;

	// Use this for initialization
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
		this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
	}

	void Update() {
		if (this.transform.position.y > 6.0f && isActive) {
			Deactivate();
		}
	}

	public void Activate() {
		isActive = true;
		float upSpeed = Random.Range (2.0f, 4.5f);
		this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, upSpeed, 0.0f);
		this.transform.position = new Vector3(Random.Range (-2.4f, 2.45f), -6.0f, 0.0f);
		if (Random.Range(0,4) == 0) {
			MakeGreen();
		} else {
			MakeNotGreen ();
		}
	}

	public void Deactivate() {
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
