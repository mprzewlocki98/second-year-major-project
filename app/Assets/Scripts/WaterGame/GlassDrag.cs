using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDrag : MonoBehaviour {

    public bool canDrag = true;
    public float speed;

    private bool isInPosition = false;
    private float distance = 10;
    private GameObject obj = null;
    private GameObject glassToBe;
    private Collider2D glassCollider;

    void Start() {

        glassToBe = GameObject.Find("glassCollider");
        glassCollider = glassToBe.GetComponent<Collider2D>();
    }

    void OnMouseDrag() { 
        
        if (canDrag) { 
            obj = GetComponent<GameObject>();
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }

    void OnMouseUp() {
        
        if (gameObject.GetComponent<Collider2D>().IsTouching(glassCollider)) {

            canDrag = false;
            gameObject.transform.position = new Vector3(glassToBe.transform.position.x, glassToBe.transform.position.y, gameObject.transform.position.z);
        }
    }
}
