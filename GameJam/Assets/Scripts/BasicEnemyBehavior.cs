using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour {
	private Vector2 lastPlayerPos;
	public GameObject player;

	public float inBetweenShootTime = 2;

	private float shootTimer = 0;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (lastPlayerPos != null) {
			shootTimer += Time.deltaTime * 1;
			if (shootTimer % inBetweenShootTime == 0) {
				// shoot projectile
				Debug.Log("projectile has been shoot");

				// update to player's current position after the shoot has been made (if the player has moved since)
				lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
			}

		}   
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			lastPlayerPos = new Vector2(player.transform.position.x, player.transform.position.y);
		}
	}



}