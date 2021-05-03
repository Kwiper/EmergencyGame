using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour {
	private Vector2 lastPlayerPos;
	public GameObject player;
<<<<<<< Updated upstream

	public float inBetweenShootTime = 2;

	private float shootTimer = 0;
=======
	public GameObject bullet;
	public float bulletSpeed = 1000f;
	private bool isWithinTrigger;
	public float fireRate = 2000f;
	
	private float shootTimer;
>>>>>>> Stashed changes
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
<<<<<<< Updated upstream
		if (lastPlayerPos != null) {
			shootTimer += Time.deltaTime * 1;
			if (shootTimer % inBetweenShootTime == 0) {
				// shoot projectile
				Debug.Log("projectile has been shoot");

				// update to player's current position after the shoot has been made (if the player has moved since)
				lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
=======
		if (isWithinTrigger) {
			if(Time.time > shootTimer) {
				Instantiate(bullet, transform.position, Quaternion.identity);
				shootTimer = Time.time + fireRate / 1000;
//				Vector2 lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
//				Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y+2);

//				Vector2 direction = enemyPos - lastPlayerPos;
//				projectile.GetComponent<Rigidbody2D>().velocity = direction * 5;
//				projectile.transform.position =
//					Vector2.MoveTowards(projectile.transform.position, direction, bulletSpeed);
>>>>>>> Stashed changes
			}

		}   
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
		}
	}



}