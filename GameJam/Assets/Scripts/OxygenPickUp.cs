using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickUp : MonoBehaviour {
	private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
	    audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
	        FindObjectOfType<OyxgenManager>().oxygenRegenPickup();
	        oxygenSound();
            Destroy(gameObject);
        }    
    }

    IEnumerator oxygenSound() {
	    audio.Play();
	    yield return new WaitForSeconds(audio.clip.length);
    }

}
