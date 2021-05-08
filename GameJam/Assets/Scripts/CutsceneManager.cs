using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutsceneManager : MonoBehaviour
{

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        
        audio.Play();
    }

    void Awake() {
	    if (PlayerPrefs.HasKey("maxVolume")) {
		    audio.volume = PlayerPrefs.GetFloat("maxVolume");
	    }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(Input.GetButton("Jump")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
