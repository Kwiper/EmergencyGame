using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenCheckPoint : MonoBehaviour
{


    private BoxCollider2D bc;
    // Start is called before the first frame update
    public void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(){
        Debug.Log("we hit something");
    }


}
