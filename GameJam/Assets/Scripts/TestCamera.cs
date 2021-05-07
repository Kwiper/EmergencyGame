using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{ 
    public GameObject player;

    //Amount that camera is normally offset by
    public float offsetNorm = 0.5f;
    
    //Amount that camera returns to norm by
    public float retractAmount = 0.2f;

    //The actual horizontal offset of the camera
    public float offsetH = 0.5f;

    //The actual vertical offset of the camera
    public float offsetV = 1f;
    //Horizontal direction the player is moving
    private float hDir;
    //Vertical direction the player is moving
    private float vDir;

    //Boolean for looking down
    private bool down;
    //Boolean for jump
    private bool jump;
    // Start is called before the first frame update

    //Might resolve the bobbing camera when we export
    public float zAxis = -4f;
    void Start()
    {
        
    }
    public void setDown(){
        down = true;
    }

    public void setJump(){
        jump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks input from player
        if(Input.GetAxis("Horizontal") != 0)hDir = Input.GetAxis("Horizontal");
        //Checks jump input from player
        if(jump == true || Input.GetAxis("Vertical") > 0){
        vDir = -1;
        jump = false;
        } 
        if(down == true){
        vDir = 1;
        down = false;
        } 
        //Resets offSet variables so that they don't increase exponentially
        offsetV = 1f;
        offsetH = 0.5f;
        //Applies greater offsets to the offsets
        offsetV += (offsetNorm*vDir);
        offsetH += (offsetNorm*hDir);
        //Camera returning to original horizontal position
        if(hDir > 0){
            //If Value is too small to be incremented back normally, will just set value to 0, prevents camera shaking
            if(Mathf.Abs(hDir) < Mathf.Abs(retractAmount))hDir = 0;
            else hDir -= retractAmount;
        }
        else if(hDir < 0){
            //If Value is too small to be incremented back normally, will just set value to 0, prevents camera shaking
            if(Mathf.Abs(hDir) < Mathf.Abs(retractAmount))hDir = 0;
            else hDir += retractAmount;          
        }
        //Camera returning to original vertical position        
        if(vDir > 0){
            //If Value is too small to be incremented back normally, will just set value to 0, prevents camera shaking            
            if(Mathf.Abs(vDir) < Mathf.Abs(retractAmount))vDir = 0;
            else vDir -= retractAmount;
        }
        else if(vDir < 0){
            //If Value is too small to be incremented back normally, will just set value to 0, prevents camera shaking           
            if(Mathf.Abs(vDir) < Mathf.Abs(retractAmount))vDir = 0;
            else vDir += retractAmount;           
        }
        transform.position = new Vector3(player.transform.position.x+offsetH, player.transform.position.y-offsetV, zAxis);
    }


}
