using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCutsceneTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    private float timer = 10;

    public AudioSource ending;

    private void Start()
    {
        ending = GetComponent<AudioSource>();
    }

    void Awake() {
	    if (PlayerPrefs.HasKey("maxVolume")) {
		    ending.volume = PlayerPrefs.GetFloat("maxVolume");
	    }
	    else {
		    ending.volume = 0.35f;
	    }

    }

    private void Update()
    {
        if (hasTriggered) {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
            Debug.Log("I happen");
            hasTriggered = true;
            // turn off oxygen counting down to prevent player from resetting to checkpoint and subsequently going to ending if player is low enough to die.
            FindObjectOfType<OyxgenManager>().oxygenCountdownToggle = false;
            ending.Play();
	    }

    }
}
