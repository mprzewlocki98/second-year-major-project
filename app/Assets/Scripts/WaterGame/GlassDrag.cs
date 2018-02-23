using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDrag : MonoBehaviour {

    public bool canDrag = true;

    private float distance = 10;
    private GameObject obj = null;

    void OnMouseDrag() { 
        
        if (canDrag) { 
            obj = GetComponent<GameObject>();
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }

    void OnMouseUp()
    {
        
    }
}
