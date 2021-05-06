using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionScript : MonoBehaviour {

	private AudioSource audio;

	private bool triggeredExit;

	private bool triggeredEnter;
    // Start is called before the first frame update
    void Start() {
	    audio = GetComponent<AudioSource>();
	    audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
	    if (triggeredEnter) {
		    if (audio.volume < 1) {
			    audio.volume += 0.3f * Time.deltaTime;
		    }
	    }
	    if (triggeredExit) {
		    if (audio.volume > 0) {
			    audio.volume -= 0.6f * Time.deltaTime;
		    }
	    }
    }

    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    triggeredEnter = true;

	    }
    }


    private void OnTriggerExit2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    triggeredExit = true;

	    }
    }
    
    
    
}
