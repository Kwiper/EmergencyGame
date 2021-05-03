using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private int range;
    private int inRange;
    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private int incrementAmount = 5;
    private bool inReverse = false;

    private Rigidbody2D rb;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other){
            isMoving = true;
            other.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other){
        isMoving = false;
        other.collider.transform.SetParent(null);
    }
    private void FixedUpdate()
    {
        //Travel up state
        if(isMoving == true){
            if(inReverse == false){
            inRange += incrementAmount;
            if(inRange >= range)inReverse = true;
            transform.position += (velocity*Time.deltaTime);
            }
            else{
            inRange -= incrementAmount;
            if(inRange <= 0)inReverse = false;
            transform.position -= (velocity*Time.deltaTime);
            }
        }  



    }
}
