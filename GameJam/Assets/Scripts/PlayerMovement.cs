using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool isAlive = true;

    private float movementInputDirection;

    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private bool isJumping;

    public float coyoteTime;
    private float coyoteCount;

    private Rigidbody2D rb;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;

    public Vector2 wallHopDirection; // Vector for wall hopping (short wall jump)
    public Vector2 wallJumpDirection; // Vector for wall jumping (normal wall jump

    public Transform groundCheck; // Checks position relative to ground
    public Transform wallCheck; // Checks position relative to wall

    public LayerMask whatIsGround; // Tests for ground layer

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckInput();
            CheckMovementDirection();
            CheckIfCanJump();
            CheckIfWallSliding();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            ApplyMovement();
            CheckSurroundings();
        }
    }

    private void CheckIfWallSliding() // Checks if player is sliding on wall
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckSurroundings() // Tests for ground or wall
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0) || isWallSliding)
        {
            canJump = true;
        }

    }

    private void CheckMovementDirection() // Checks for player direction
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (rb.velocity.x != 0)
        {
            isWalking = true;
            
        }
        else
        {
            isWalking = false;
        }
    }

    private void CheckInput() // Checks for inputs
    {
        movementInputDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

    }

    // Jump logic
    private void Jump()
    {
        if (canJump && !isWallSliding && coyoteCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            FindObjectOfType<OyxgenManager>().jumpDeplete();
            FindObjectOfType<TestCamera>().setJump();
        }
        else if (isWallSliding && movementInputDirection == 0 && canJump) //Wall hop
        {
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            isJumping = true;
            FindObjectOfType<OyxgenManager>().wallHopDeplete();
        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump) // Wall jump
        {
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            isJumping = true;
            FindObjectOfType<OyxgenManager>().jumpDeplete();
        }
    }

    // Movement logic
    private void ApplyMovement()
    {

        if (isGrounded)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            coyoteCount = coyoteTime;
            isJumping = false;
            FindObjectOfType<OyxgenManager>().moveDeplete();
        }
        //For Coyote Time Movement
        if (!isGrounded && !isJumping && !isWallSliding) {
            coyoteCount -= Time.deltaTime;
            FindObjectOfType<OyxgenManager>().moveDeplete();
        }
        //Air Force movement
        else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                isJumping = false;
            }
        }
    }
    
    // Flips player direction
    private void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    // Sets player to living
    public void setIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
        Debug.Log("Player is dead");
    }
}