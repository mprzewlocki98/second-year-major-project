using UnityEngine;

public class DragItem : MonoBehaviour {
	private float decaySecond = 1f;
	private float elaspedSecond = 0;
	private float distance = 10;
	private bool flag = false;
	private GameObject obj;

	public AudioClip clickSound;
	private AudioSource clickSource;

	void Awake(){
		this.gameObject.AddComponent<AudioSource> ();
		clickSource = this.gameObject.GetComponent<AudioSource> ();
		clickSource.clip = clickSound;
		clickSource.playOnAwake = false;
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
	}

	void OnMouseDown(){
		clickSource.Play ();
	}

	void OnMouseUp () {
		flag = true;
		Destroy(this.gameObject, decaySecond); // destroy the object once it is dragged off
	}

	void Update(){
		if (flag) {
			float scaleRate = (decaySecond - elaspedSecond) / decaySecond;
			transform.localScale *= scaleRate;
			elaspedSecond += Time.deltaTime;
		}
			
	}
}
