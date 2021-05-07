using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OyxgenManager : MonoBehaviour {
	public float oxygen = 1000;
	public float maxOxygen;
	
	public float oxygenCheckPointMin = 500;
	
	// stores the last value of oxygen at a checkpoint
	public float oxygenLastCheck;

	// modify this to change the amount of oxygen you lose passively
	public float passiveSubtractTime = 1;
	
	// modify this to change the amount of oxygen you lose during jumping
	public float jumpSubtractTime = 2;

	// modify this to change the amount of oxygen you lose during wall hopping
	public float wallHopSubtractTime = 1.3f;

	// modify this to change the amount of oxygen you lose during movement
	public float moveSubtractTime = 1.5f;
	
	// modify this to change the amount of oxygen you lose by getting hit
	public float hitByEnemySubtractTime = 100;
	
	// modify this to change the amount of oxygen you regain per pickup
	public float oxygenRegenPickupTime = 25;
	
	// modify this to change the amount of oxygen you regain per checkpoint
	public float oxygenRegenCheckpointTime = 50;

	private GameObject[] MovingPlatformArr;

	public bool oxygenCountdownToggle;

	public OxygenBar oxygenBar;
	public TextMeshProUGUI percentageText;
	
	void Start() {
		maxOxygen = oxygen;
        oxygenBar.SetMaxOxygen(oxygen);
        oxygenLastCheck = oxygen;
    }
    
    void Update() {
	    oxygenBar.SetOxygen(oxygen);
	    float currentPercentage = (oxygen / maxOxygen) * 100;
	    string currentPercentageString = currentPercentage.ToString("F1");
	    percentageText.text = currentPercentageString + "%";
	    
	    
	    if (oxygen <= 0) {
		    FindObjectOfType<PlayerMovement>().ResetPlayerToLastCheckpoint();
		    oxygen = oxygenLastCheck;
		    MovingPlatformArr = GameObject.FindGameObjectsWithTag("MovingPlatform");
		    foreach (GameObject movingPlatform in MovingPlatformArr) {
			    movingPlatform.GetComponent<MovingPlatform>().ResetBackToStart();
		    }
		    


	    }
    }

    private void FixedUpdate() {
	    if(oxygenCountdownToggle) oxygen -= Time.deltaTime * passiveSubtractTime;
    }

    public void jumpDeplete() {
	    if(oxygenCountdownToggle) oxygen -= jumpSubtractTime;
    }


    public void wallHopDeplete() {
	    if(oxygenCountdownToggle) oxygen -= jumpSubtractTime;
    }

    public void moveDeplete() {
		if(oxygenCountdownToggle) oxygen -= moveSubtractTime;
    }

    public void oxygenRegenPickup() {
	    oxygen += oxygenRegenPickupTime;
    }

    public void oxygenRegenCheckpoint() {

	    if (oxygen > oxygenCheckPointMin) {
		    oxygen += oxygenRegenCheckpointTime;
	    }
	    else {
		    oxygen = oxygenCheckPointMin;
	    }
	    // Every time you land on a checkpoint, save that oxygen level. 
	    oxygenLastCheck = oxygen;
		
	    
    }


//	public void oxygenSaveCheckPoint() {
//	    
//    }

	public void hitDelete() {
		StartCoroutine(PlayerHit());
		if (oxygenCountdownToggle) {
			oxygen -= hitByEnemySubtractTime;
			
		}
	}
	
	IEnumerator PlayerHit() {
		GameObject player = GameObject.Find("Player");
		player.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.05f);
		player.GetComponent<SpriteRenderer>().color = Color.white;

	}
}
