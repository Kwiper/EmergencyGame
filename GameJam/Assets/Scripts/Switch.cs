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

    private Animator switch_anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        switch_anim = gameObject.GetComponent<Animator>();
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
        Animate();
        if (inRange && !toggled){
            //GameManager set state
            FindObjectOfType<GameManager>().setState();
            toggled = true;
        }
    }

    private void Animate() {
        if (FindObjectOfType<GameManager>().state)
        {
            switch_anim.SetBool("isGreen", true);
        }
        else {
            switch_anim.SetBool("isGreen", false);
        }
    }
}
