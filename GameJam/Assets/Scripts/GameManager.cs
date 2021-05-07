using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   //On and off switches boolean
    public bool state = false;

    //Controls the state of the on and off switches
    public void setState(){

            if(state){
                state = false;
            }
            else{
                state = true;
            }

    }



}
