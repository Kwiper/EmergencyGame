using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed; // Player speed
    public float jumpForce; // Player jump height
    private bool isAlive = true; // Player's living status
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float coyoteTime;
    private float coyoteCounter;

    private float h; //Horizontal movement
    private Rigidbody2D rb;

    private bool isGrounded; // Tests if player is touching the ground
    private bool isTouchingLeft; // Tests if player is touching wall on left
    private bool isTouchingRight; // Tests if player is touching wall on right
    private bool isTouchingWall; // Tests if touching wall


    public float checkRadius;
    public LayerMask whatIsGround; // Checks for objects in the Ground layer (Layer 8)
    public LayerMask lastSurface; //stores last touched surface for wall jump

    // Wall Jumping
    public float xWallForce; // x-axis wall jump force
    public float wallJumpTime = 2f; // Wall jump time
    private float wallJumpCounter; // Wall Jump counter


    public Transform feetPos;
    public Transform leftPos;
    public Transform rightPos;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isAlive == true){
            if (wallJumpCounter <= 0) // Tests if wall jump counter is less than or equal to 0 (not in wall jump state)
            {
                isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); // Defines touching gound
                isTouchingLeft = Physics2D.OverlapCircle(leftPos.position, checkRadius, whatIsGround);
                isTouchingRight = Physics2D.OverlapCircle(rightPos.position, checkRadius, whatIsGround);

                // Normal jumping

                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    rb.velocity = Vector2.up * jumpForce;
                    FindObjectOfType<OyxgenManager>().jumpDeplete();
                }

                if (Input.GetKey(KeyCode.Space) && isJumping)
                {
                    if (jumpTimeCounter > 0)
                    {
                        rb.velocity = Vector2.up * jumpForce;
                        jumpTimeCounter -= Time.deltaTime;
                    }
                    else
                    {
                        isJumping = false;
                    }

                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isJumping = false;
                }

                if (rb.velocity.y > 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

                // Coyote Time
                if (isGrounded)
                {
                    coyoteCounter = coyoteTime;
                }

                if (!isJumping && !isGrounded) {
                    coyoteCounter -= Time.deltaTime;
                }

                if (!isJumping && !isGrounded && coyoteCounter > 0 && Input.GetKeyDown(KeyCode.Space)) {
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    rb.velocity = Vector2.up * jumpForce;
                    FindObjectOfType<OyxgenManager>().jumpDeplete();
                }


                // Wall jumping

                if (!isGrounded && (isTouchingLeft || isTouchingRight)) // Wall touching detection
                {
                    if (Input.GetAxisRaw("Horizontal") > 0 && lastSurface != whatIsGround || 
                        Input.GetAxisRaw("Horizontal") < 0 && lastSurface != whatIsGround) // Basically you can only wall jump if you're holding into the wall
                    {
                        isTouchingWall = true;
                    }
                    else
                    {
                        isTouchingWall = false;
                    }
                }
                else
                {
                    isTouchingWall = false;
                }

                //Debug.Log(isTouchingWall);

                if (isTouchingWall) // Wall jump logic
                {
                    if (Input.GetButtonDown("Jump")) {
                        wallJumpCounter = wallJumpTime;
                        rb.velocity = new Vector2(-h * xWallForce * speed * Time.deltaTime, jumpForce + 5);
                        if (lastSurface != whatIsGround) lastSurface = whatIsGround;
                    }
                }
            }
            else {
                wallJumpCounter -= Time.deltaTime;
            }
        }

    }

    void FixedUpdate()
    {
        if(isAlive == true){
            h = Input.GetAxis("Horizontal");
            if(h != 0)FindObjectOfType<OyxgenManager>().moveDeplete();
        }
        if (wallJumpCounter <= 0)
        {
            rb.velocity = new Vector2(h * speed * Time.deltaTime, rb.velocity.y);
        }
    }

    public void setIsAlive(bool isAlive){
        this.isAlive = isAlive;
        h = 0;
        Debug.Log("Player is dead");
    }

}
