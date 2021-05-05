using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyxgenManager : MonoBehaviour {
	public float oxygen = 1000;

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

    public void oxygenRegenPickup() {
	    oxygen += oxygenRegenPickupTime;
    }

    public void oxygenRegenCheckpoint() {
		
	    if(oxygen > oxygenCheckPointMin)oxygen += oxygenRegenCheckpointTime;
		else oxygen = oxygenCheckPointMin;
    }


	public void oxygenSaveCheckPoint() {
	    oxygenLastCheck = oxygen;
    }

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
