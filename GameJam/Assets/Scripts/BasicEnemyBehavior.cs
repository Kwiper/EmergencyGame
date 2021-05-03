using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
//		Vector2 lastPlayerPos = player.transform.position;
		if (isWithinTrigger) {
//			RaycastHit2D hit =
//				Physics2D.Raycast(transform.position, lastPlayerPos, 8);
//			Debug.DrawRay(transform.position, GameObject.FindWithTag("Player").transform.position, Color.black);
//			if (hit.collider == GameObject.FindWithTag("Player")) {
				
				Debug.Log("raycast has collided with player");
				if (Time.time > shootTimer) {
					Instantiate(bullet, transform.position, Quaternion.identity);
					shootTimer = Time.time + fireRate / 1000;
					//				Vector2 lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
					//				Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y+2);

					//				Vector2 direction = enemyPos - lastPlayerPos;
					//				projectile.GetComponent<Rigidbody2D>().velocity = direction * 5;
					//				projectile.transform.position =
					//					Vector2.MoveTowards(projectile.transform.position, direction, bulletSpeed);
				}
//			}
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