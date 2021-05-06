using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPoints : MonoBehaviour
{
    public MovingPlatform movingPlatform;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        //Prevents the triggers from firing from anything other than the moving platform
        if(other.gameObject != player && other.gameObject.tag != "Bullet") movingPlatform.setInReverse();
    }
}
