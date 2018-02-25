using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    public GameObject[] waterArray;
    private int nextWaterComponent = 0;

	void Start () {
		
        for (int i = 0; i < waterArray.Length; i++) {

            waterArray[i].SetActive(false);
        }
	}
	
    void OnMouseDown(){

        if (nextWaterComponent < waterArray.Length) {

            waterArray[nextWaterComponent].SetActive(true);
            nextWaterComponent++;
        }
    }
	
	void Update () {
		
	}
}
