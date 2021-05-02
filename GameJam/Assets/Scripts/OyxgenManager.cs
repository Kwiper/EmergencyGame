using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyxgenManager : MonoBehaviour {
	public float oxygen = 1000;
	
	// modify this to change the amount of oxygen you lose passively
	public float passiveSubtractTime = 1;
	
	// modify this to change the amount of oxygen you Lose during jumping
	public float jumpSubtractTime = 2;
	
	// modify this to change the amount of oxygen you Lose during movement
	public float moveSubtractTime = 1.5f;
	
	// modify this to change the amount of oxygen you regain per pickup
	public float oxygenRegenPickupTime = 25;
	
	// modify this to change the amount of oxygen you regain per checkpoint
	public float oxygenRegenCheckpointTime = 50;

    void Start()
    {
        
    }
    
    void Update() {
	    oxygen -= Time.deltaTime * passiveSubtractTime;
		Debug.Log(oxygen);
    }

    public void jumpDeplete() {
	    oxygen -= jumpSubtractTime;
    }

    public void moveDeplete() {
	    oxygen -= moveSubtractTime;
    }

    public void oxygenRegenPickuo() {
	    oxygen += oxygenRegenPickupTime;
    }

    public void oxygenRegenCheckpoint() {
	    oxygen += oxygenRegenCheckpointTime;
    }
}
