using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    //First time toggle
    private bool toggled = false;
    
	private float intervalTimeInMilliseconds = 500f;
    private float toggleTime;
    //public GameObject objectToChange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other == player.GetComponent<Collider2D>()) {
	        if (Time.time > toggleTime) {
		        inRange = true;
                //Delay time
		        toggleTime = Time.time + intervalTimeInMilliseconds / 1000;
	        }
            
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other == player.GetComponent<Collider2D>()){
            inRange = false;
            toggled = false;
        }
    }
    void Update(){
        if(inRange && !toggled){
            //GameManager set state
            FindObjectOfType<GameManager>().setState();
            toggled = true;
        }
    }
}
