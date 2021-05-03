using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private bool state = true;
    public GameObject objectToChange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other == player.GetComponent<Collider2D>()){
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other == player.GetComponent<Collider2D>()){
            inRange = false;
        }
    }
    void Update(){
        bool enterIsPressed = Input.GetButtonDown("Submit");
        if(inRange == true && enterIsPressed == true){
            if(state == true){
                objectToChange.GetComponent<SpriteRenderer>().enabled=false;
                state = false;
            }
            else{
                objectToChange.GetComponent<SpriteRenderer>().enabled=true;
                state = true;
            }
        }
    }
}
