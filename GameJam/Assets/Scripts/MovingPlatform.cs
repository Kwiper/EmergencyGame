using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private int range;

    [SerializeField]
    private LayerMask targetLayer;
    private int inRange;
    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private float speed = 5f;
    private bool inReverse = false;

    [SerializeField]
    private GameObject pointA;

    [SerializeField]
    private GameObject pointB;

    [SerializeField]
    private int detectRadius;
    private Rigidbody2D rb;
    private Transform tr;

    private BoxCollider2D bc;
    private bool isMoving;

    private bool hit;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
       
    }

    private void OnCollisionEnter2D(Collision2D other){
            isMoving = true;
            other.collider.transform.SetParent(transform);

            //if(other == pointB.GetComponent<Collision2D>())inReverse = true;
            //else if(other == pointA.GetComponent<Collision2D>())inReverse = false;
    }

    private void OnCollisionExit2D(Collision2D other){
            isMoving = false;
            other.collider.transform.SetParent(null);

    }

    public void setInReverse(){
        if(inReverse)inReverse = false;
        else inReverse = true;
    }

    private void FixedUpdate()
    {
        //Travel up state
        if(isMoving == true){
            // if(inReverse == false){
            // inRange += incrementAmount;
            // if(inRange >= range)inReverse = true;
            // transform.position += (velocity*Time.deltaTime);
            // }
            // else{
            // inRange -= incrementAmount;
            // if(inRange <= 0)inReverse = false;
            // transform.position -= (velocity*Time.deltaTime);
            // }
             if(inReverse == false){
                velocity = (pointB.GetComponent<Transform>().position - transform.position).normalized * speed;
                transform.position += (velocity*Time.deltaTime);

             }
             else if(inReverse == true){
                velocity = (pointA.GetComponent<Transform>().position- transform.position).normalized * speed;    
                transform.position += (velocity*Time.deltaTime);
             }

        }  



    }
}
