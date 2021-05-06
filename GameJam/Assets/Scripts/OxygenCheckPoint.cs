using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenCheckPoint : MonoBehaviour {
	private AudioSource audio;
    private bool activated = false;
    private BoxCollider2D bc;

    private string lastCheckpointName;
    // Start is called before the first frame update
    public void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Player")){
            if(activated == false){
	            audio.Play();
		        FindObjectOfType<OyxgenManager>().oxygenRegenCheckpoint();
		//            FindObjectOfType<OyxgenManager>().oxygenSaveCheckPoint();
		        activated = true;
		        lastCheckpointName = gameObject.name;
		        GameObject player = GameObject.Find("Player");
		        player.GetComponent<PlayerMovement>().SetLastPlayerPos(transform.position);
            

            }
        }
    }


}
