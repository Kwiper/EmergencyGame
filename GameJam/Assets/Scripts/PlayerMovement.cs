using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Never underestimate the challenge of making good player movement.

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
	
	// This animator is for the player's animations
	private Animator player_anim;

	private Vector2 lastPlayerPos;

	public AudioClip silence;
	public AudioClip walking;
	
	public AudioClip walljump;
	public AudioClip jump;
	public AudioClip hit;

	private AudioSource playerAudio;

	private float maxVolume;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        player_anim = gameObject.GetComponent<Animator>();
        lastPlayerPos = transform.position;

        playerAudio = GetComponent<AudioSource>();
        
        playerAudio.Play();
        
    }
	
    void Awake() {
	    if(PlayerPrefs.HasKey("maxVolume")){	
		    maxVolume = PlayerPrefs.GetFloat("maxVolume");
	    }
	    else {
		    maxVolume = 0.35f;
	    }
    }
    
    // Update is called once per frame
    void Update() {
	    playerAudio.volume = 0.75f;
	    //Debug.Log(playerAudio.isPlaying);
        if (isAlive)
        {
	        if (Input.GetAxis("Horizontal") == 0) {
		        FindObjectOfType<OyxgenManager>().passiveDeplete();
	        }
	        
            CheckInput();
            CheckMovementDirection();
            CheckIfCanJump();
            CheckIfWallSliding();
            Animate();
            PlayAudio();
            if (rb.velocity.y < 0) {
	            FindObjectOfType<OyxgenManager>().fallDeplete();
            }
            
//            Debug.Log(facingDirection);
//            Debug.Log(movementInputDirection);

        }

        
        
        
        if (!playerAudio.isPlaying) {
	        playerAudio.Play();
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
    }

    private void CheckInput() // Checks for inputs
    {
        movementInputDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            FindObjectOfType<OyxgenManager>().jumpDeplete();
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
            
            FindObjectOfType<TestCamera>().setJump();
            coyoteCount = 0;
        }
        else if (isWallSliding && movementInputDirection == 0 && canJump) //Wall hop
        {
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            isJumping = true;
            FindObjectOfType<OyxgenManager>().wallHopDeplete();
            Debug.Log("Wall hop");
        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump) // Wall jump
        {

            if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
            {
                isWallSliding = false;
                Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
                rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                isJumping = true;
                FindObjectOfType<OyxgenManager>().jumpDeplete();
                Debug.Log("Jump Deplete");
            }
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

        }
        //For Coyote Time Movement
        if (!isGrounded && !isJumping && !isWallSliding) {
            coyoteCount -= Time.deltaTime;

        }

        if (!isGrounded && !isWallSliding && movementInputDirection != 0) {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
        //Air Force movement
        /*
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
        */

        if (isWallSliding)
        {
            coyoteCount = 0;
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

    private void Animate() {
	    if(movementInputDirection != 0 && !isWallSliding) {
		    player_anim.SetBool("isMoving", true);
		    FindObjectOfType<OyxgenManager>().moveDeplete();
		    
	    }
	    else {
		    player_anim.SetBool("isMoving", false);
		    
	    }
        if (isWallSliding && !isGrounded)
        {
	        player_anim.SetBool("isWallSliding", true);
	        FindObjectOfType<OyxgenManager>().slideDeplete();
	        
        }
        else
        {
	        
            player_anim.SetBool("isWallSliding", false);
        }

        if (movementInputDirection == 0 && isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            player_anim.SetBool("isCrouching", true);
            FindObjectOfType<TestCamera>().setDown();
        }
        else
        {
            player_anim.SetBool("isCrouching", false);
        }

        if (!isGrounded && rb.velocity.y != 0 && !isWallSliding)
        {
	        
            player_anim.SetBool("isJumping", true);
            
        }
        else {
	        
            player_anim.SetBool("isJumping", false);
        }

        


    }

    private void PlayAudio() {
	    if (movementInputDirection != 0 && !isWallSliding && isGrounded) {
		    playerAudio.clip = walking;
	    }
	    else {
		    playerAudio.clip = silence;
	    }

	    if (Input.GetButtonDown("Jump")&& isGrounded) {
		    playerAudio.PlayOneShot(jump);
	    }
	    
    }

    public void SetLastPlayerPos(Vector2 position) {
	    lastPlayerPos = position;
    }

    public void ResetPlayerToLastCheckpoint() {
	    transform.position = lastPlayerPos;
    }
    
}