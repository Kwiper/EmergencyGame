using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    public bool state = false;
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
        //bool enterIsPressed = Input.GetButtonDown("Submit");
        if(inRange && toggled == false){
            if(state){
                //objectToChange.GetComponent<SpriteRenderer>().enabled=false;
                state = false;
            }
            else{
                //objectToChange.GetComponent<SpriteRenderer>().enabled=true;
                state = true;
            }
            toggled = true;
        }
    }
}
