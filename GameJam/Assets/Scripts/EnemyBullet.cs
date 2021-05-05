using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	private Rigidbody2D rb;
	private float speed = 5f;
	private GameObject player;
	private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start() {
	    player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
//        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    Debug.Log("WHAT");
		    //FindObjectOfType<OxygenManager>().oxygenHit();
		    Destroy(gameObject);
	    }
	    else if (!other.gameObject.CompareTag("Player")) {
		    Destroy(gameObject);
	    }

    }
}
