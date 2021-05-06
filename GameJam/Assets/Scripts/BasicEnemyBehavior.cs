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
			if(Time.time > shootTimer) {
				Instantiate(bullet, transform.position, transform.rotation);
				shootTimer = Time.time + fireRate / 1000;
//				Vector2 lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
//				Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y+2);

//				Vector2 direction = enemyPos - lastPlayerPos;
//				projectile.GetComponent<Rigidbody2D>().velocity = direction * 5;
//				projectile.transform.position =
//					Vector2.MoveTowards(projectile.transform.position, direction, bulletSpeed);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
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