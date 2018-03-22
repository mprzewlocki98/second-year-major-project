using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static int frames;
	void Start () {
        GetComponent<AudioSource>().time = frames/60;
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        frames++;
	}
}
