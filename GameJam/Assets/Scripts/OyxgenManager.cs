using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyxgenManager : MonoBehaviour {
	public float oxygen = 1000;
	
	// stores the last value of oxygen at a checkpoint
	public float oxygenLastCheck;

	// modify this to change the amount of oxygen you lose passively
	public float passiveSubtractTime = 1;
	
	// modify this to change the amount of oxygen you Lose during jumping
	public float jumpSubtractTime = 5;
	
	// modify this to change the amount of oxygen you Lose during movement
	public float moveSubtractTime = 1.5f;
	
	// modify this to change the amount of oxygen you regain per pickup
	public float oxygenRegenPickupTime = 25;
	
	// modify this to change the amount of oxygen you regain per checkpoint
	public float oxygenRegenCheckpointTime = 50;

	public bool oxygenCountdownToggle = false;

    void Start()
    {
        
    }
    
    void Update() {
	    if(oxygenCountdownToggle) oxygen -= Time.deltaTime * passiveSubtractTime;
		if(oxygen <= 0)FindObjectOfType<PlayerMovement>().setIsAlive(false);
		
		
		
    }

    public void jumpDeplete() {
		if (oxygenCountdownToggle) oxygen -= jumpSubtractTime;
    }

    public void moveDeplete() {
		if (oxygenCountdownToggle) oxygen -= moveSubtractTime;
    }

    public void oxygenRegenPickuo() {
	    oxygen += oxygenRegenPickupTime;
		if (oxygen > 1000) oxygen = 1000;
    }

    public void oxygenRegenCheckpoint() {
	    oxygen += oxygenRegenCheckpointTime;
		if (oxygen > 1000) oxygen = 1000;
	}

	public void oxygenSaveCheckPoint() {
	    oxygenLastCheck = oxygen;
    }
}
