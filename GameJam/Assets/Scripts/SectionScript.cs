using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionScript : MonoBehaviour {

	private AudioSource audio;

	private float maxVolume;
	public float overrideMaxVolume;
	private bool triggeredEnter;

	public bool lastSection;
    // Start is called before the first frame update
    void Start() {
		
	    audio = GetComponent<AudioSource>();
	    audio.Play();
    }

    void Awake() {
	    if(PlayerPrefs.HasKey("maxVolume")){	
		    maxVolume = PlayerPrefs.GetFloat("maxVolume");
	    }
	    else {
		    maxVolume = overrideMaxVolume;
	    }
    }



    // Update is called once per frame
    void Update()
    {
	    if (triggeredEnter) {
		    if (gameObject.name == "Section 1") {
			    audio.volume = maxVolume;
		    }
		    else if (audio.volume < maxVolume) {
			    audio.volume += (maxVolume * 0.6f) * Time.deltaTime;
		    }
	    }
	    else if (!triggeredEnter) {
		    if (lastSection) {
			    audio.volume = 0;
		    }
		    else if (audio.volume > 0) {
			    audio.volume -= (maxVolume * 0.6f) * Time.deltaTime;
		    }
		    
	    }
	    
    }

//    private void OnTriggerEnter2D(Collider2D other) {
//	    if (other.gameObject.CompareTag("Player")) {
//		    triggeredEnter = true;
//
//	    }
//    }
//
//
//    private void OnTriggerExit2D(Collider2D other) {
//	    if (other.gameObject.CompareTag("Player")) {
//		    triggeredExit = true;
//
//	    }
//    }

    private void OnTriggerStay2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    triggeredEnter = true;
	    }
    }

    private void OnTriggerExit2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    triggeredEnter = false;
	    }
    }
    
    
}
