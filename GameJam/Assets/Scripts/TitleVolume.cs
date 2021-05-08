using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleVolume : MonoBehaviour {
	private float maxVolume;

	public AudioSource source;
    // Start is called before the first frame update
    void Start() {
	    source.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (PlayerPrefs.HasKey("maxVolume")) {
		    maxVolume = PlayerPrefs.GetFloat("maxVolume");
	    }
	    else {
		    maxVolume = 0.35f;
	    }

	    source.volume = maxVolume;
    }
}
