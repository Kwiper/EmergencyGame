using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed; // Player speed
    public float jumpForce; // Player jump height

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float h; //Horizontal movement
    private Rigidbody2D rb;

    private bool isGrounded; // Tests if player is touching the ground
    private bool isTouchingLeft; // Tests if player is touching wall on left
    private bool isTouchingRight; // Tests if player is touching wall on right
    private bool isTouchingWall; // Tests if touching wall


    public float checkRadius;
    public LayerMask whatIsGround; // Checks for objects in the Ground layer (Layer 8)

    // Wall Jumping
    private bool isWallJumping; // Checks if player is wall jumping
    public float xWallForce; // x-axis wall jump force
    public float yWallForce; // y-axis wall jump force
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
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); // Defines touching gound
        isTouchingLeft = Physics2D.OverlapCircle(leftPos.position, checkRadius, whatIsGround);
        isTouchingRight = Physics2D.OverlapCircle(rightPos.position, checkRadius, whatIsGround);

        // Normal jumping

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
            FindObjectOfType<OyxgenManager>().jumpDeplete();
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping) {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else {
                isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }

        if (rb.velocity.y > 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Wall jumping

        if (!isGrounded && (isTouchingLeft || isTouchingRight))
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
            {
                isTouchingWall = true;
            }
            else {
                isTouchingWall = false;
            }
        }
        else {
            isTouchingWall = false;
        }

        //Debug.Log(isTouchingWall);

        if (isTouchingWall) {
            wallJumpCounter = wallJumpTime;
        }


    }

    void FixedUpdate()
    {

        h = Input.GetAxis("Horizontal");
        if(h != 0)FindObjectOfType<OyxgenManager>().moveDeplete();

        if (wallJumpCounter <= 0) 
        {
            rb.velocity = new Vector2(h * speed * Time.deltaTime, rb.velocity.y);
        }
    }

    void SetWallJumpToFalse() {
        isWallJumping = false;
    }
}
