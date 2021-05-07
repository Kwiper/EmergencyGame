using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour {
	
	public GameObject player;
	public GameObject bullet;
	public float bulletSpeed = 1000f;
	private bool isWithinTrigger;
	public float fireRate = 2000f;
	
	private float shootTimer;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isWithinTrigger) {
			// fire at player every "firerate" milliseconds
			if(Time.time > shootTimer) {
				Instantiate(bullet, transform.position, transform.rotation);
				shootTimer = Time.time + fireRate / 1000;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// if the player has collided with the area of which the enemy operates at
		if (other.CompareTag("Player")) {
			Debug.Log("Collide");
			isWithinTrigger = true;
			
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			Debug.Log("Exit Collide");
			isWithinTrigger = false;
		}
	}
}