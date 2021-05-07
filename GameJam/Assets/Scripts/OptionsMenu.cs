using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour
{   


    public void adjustLevel(float SliderValue){

        PlayerPrefs.SetFloat("maxVolume",SliderValue);
    }

}
