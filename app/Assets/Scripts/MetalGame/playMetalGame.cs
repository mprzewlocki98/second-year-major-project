using UnityEngine;

public class playMetalGame : MonoBehaviour {
	float hintSecond = 1f;
	float speed = 10f; //how fast it shakes
	float amount = 2f;//how much it shakes
	bool processEnd = true;

    private bool easyMode = Difficulty.easyMode;

    Vector3 mousePos;

	// Use this for initialization
	void Start () {
		hintSecond = Time.time;
		mousePos = new Vector3 (0,0,0);
	}
		
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.FindWithTag ("item");

		if (obj != null && Time.time - hintSecond >= 5) {
			
			SpriteRenderer SR = obj.GetComponent<SpriteRenderer> ();

			if (amount > 0) {
				processEnd = false;
				SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, Mathf.Sin (Time.time * speed));
				amount -= Time.deltaTime;

			} else {

				SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, 1);
				amount = 2f;
				hintSecond = Time.time;
				processEnd = true;
			}	
		}
			
		if (Input.mousePosition != mousePos && processEnd) {
			amount = 2f;
			hintSecond = Time.time;
			mousePos = Input.mousePosition;
		}
	}
}