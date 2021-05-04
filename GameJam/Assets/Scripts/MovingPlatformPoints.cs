using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPoints : MonoBehaviour
{
    public MovingPlatform movingPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(){
        movingPlatform.setInReverse();
    }



}
