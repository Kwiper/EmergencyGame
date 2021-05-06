using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private string currentSection;
	public AudioClip section1;
	public AudioClip section2;
	public AudioClip section3;
	public AudioClip section4;
	public AudioSource source;
	
    // Start is called before the first frame update
    void Start() {
	    source = GetComponent<AudioSource>();
	    source.Play();
    }

    // Update is called once per frame
    void Update() {
	    playManager();
	    
    }


    private void playManager() {
	    if (currentSection == "Section 1") {
		    source.clip = section1;

	    }
	    if (currentSection == "Section 2") {
		    source.clip = section2;
		    source.Play();

	    }
	    
    }

    public void SetCurrentSection(string current) {
	    currentSection = current;
    }

    
}
