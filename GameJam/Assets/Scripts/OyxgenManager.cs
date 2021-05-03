using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyxgenManager : MonoBehaviour {
	public float oxygen = 1000;
	
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
	
	// modify this to change the amount of oxygen you regain per pickup
	public float oxygenRegenPickupTime = 25;
	
	// modify this to change the amount of oxygen you regain per checkpoint
	public float oxygenRegenCheckpointTime = 50;

	public bool oxygenCountdownToggle = false;

	public OxygenBar oxygenBar;
	
	void Start()
    {
        oxygenBar.SetMaxOxygen(oxygen);
    }
    
    void Update() {
	    oxygenBar.SetOxygen(oxygen);
	    if(oxygenCountdownToggle) oxygen -= Time.deltaTime * passiveSubtractTime;
		if(oxygen <= 0)FindObjectOfType<PlayerMovement>().setIsAlive(false);
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

    public void oxygenRegenPickuo() {
	    oxygen += oxygenRegenPickupTime;
    }

    public void oxygenRegenCheckpoint() {
	    oxygen += oxygenRegenCheckpointTime;
    }

	public void oxygenSaveCheckPoint() {
	    oxygenLastCheck = oxygen;
    }
}
