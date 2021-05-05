using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenCheckPoint : MonoBehaviour
{

    private bool activated = false;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    public void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Player")){
            if(activated == false){
            FindObjectOfType<OyxgenManager>().oxygenRegenCheckpoint();
            FindObjectOfType<OyxgenManager>().oxygenSaveCheckPoint();
            activated = true;
            }
        }
    }


}
