using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    public GameObject[] waterArray;

    private int nextWaterComponent = 0;
    private GameObject glass;
    private GlassDrag glassScript;

	void Start () {

        glass = GameObject.Find("glass");
        glassScript = glass.GetComponent<GlassDrag>();

        for (int i = 0; i < waterArray.Length; i++) {

            waterArray[i].SetActive(false);
        }
	}
	
    void OnMouseDown(){

        if (glassScript.IsAtPosition()) { 

            if (nextWaterComponent < waterArray.Length) {

                waterArray[nextWaterComponent].SetActive(true);
                nextWaterComponent++;
            }
        }
    }
	
	void Update () {
		
	}
}
