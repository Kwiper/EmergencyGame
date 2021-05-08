using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour {
	
	private Vector3 playerPos;
	public GameObject bullet;
	public float bulletSpeed = 1000f;
	private bool isWithinTrigger;
	public float fireRate = 2000f;
	public GameObject muzzle;
	private float shootTimer;
	public Sprite offSprite;
	public Sprite onSprite;
	public Sprite onBase;
	public Sprite offBase;
	private SpriteRenderer spriteRenderer;
	public GameObject turretBaseObject;
	private SpriteRenderer turretBase;
	private float maxVolume;
	public GameObject gunHead;

	public AudioClip shootSound;

	private AudioSource turretSource;
	// Start is called before the first frame update
	void Start()
	{
		
		
		var muzzleTransformPosition = muzzle.transform.position;
		muzzleTransformPosition.z = 0;
		
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		turretBase = turretBaseObject.GetComponent<SpriteRenderer>();
		turretSource = GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update() {
		
		if (isWithinTrigger) {
			// rotate towards player
			spriteRenderer.sprite = onSprite;
			turretBase.sprite = onBase;
			Vector3 targetDirection = playerPos - gunHead.transform.position;
			float angle = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			
//			Vector2 newDirection = Vector3.RotateTowards(transform.up, targetDirection, 15, 0.0f);
			gunHead.transform.rotation = Quaternion.Slerp(gunHead.transform.rotation, rotation, 3f * Time.deltaTime);
			
			// fire at player every "firerate" milliseconds
			if(Time.time > shootTimer) {
				shootTimer = Time.time + fireRate / bulletSpeed;
				Instantiate(bullet,muzzle.transform.position,muzzle.transform.rotation);
				turretSource.PlayOneShot(shootSound);
				
			}
		}
		else {
			spriteRenderer.sprite = offSprite;
			turretBase.sprite = offBase;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// if the player has collided with the area of which the enemy operates at
		if (other.CompareTag("Player")) {
			
			
			
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			isWithinTrigger = true;
			playerPos = other.transform.position;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			Debug.Log("Exit Collide");
			isWithinTrigger = false;
		}
	}
}