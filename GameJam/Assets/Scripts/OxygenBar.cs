using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OxygenBar : MonoBehaviour {
	public Slider slider;
	
    // Start is called before the first frame update
    public void SetOxygen(float oxygen) {
	    slider.value = oxygen;
    }

    public void SetMaxOxygen(float oxygen) {
	    slider.maxValue = oxygen;
	    slider.value = oxygen;
    }
    
}
