using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool jumpQueued = false;
    private bool isFacingRight = true;
    private float airTime = 0.0f;
    private float jumpAnimTime = 0.0f;
    private Vector2 groundCheckSize = new Vector2(0.45f , 0.1f);
    private Vector2 queueCheckSize = new Vector2(0.45f, 2f);
    public float playerMaxHealth = 8.0f;
    public float playerHealth = 8.0f;
    public int iFrames = 0;
    public float playerKBTime = 0.0f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform bufferCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    private void Awake()
    {
        playerMaxHealth = 8.0f;
        playerHealth = 8.0f;
    }
    void Update()
    {
        
        airTime += Time.deltaTime;
        jumpAnimTime += Time.deltaTime;
        playerKBTime -= Time.deltaTime;
        
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        if (iFrames > 0)
        {
            iFrames--;
        }
        if (iFrames < 0)
        {
            iFrames = 0;
        }
        if(playerKBTime<=0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if(Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                jumpAnimTime = 0.0f;
                animator.SetTrigger("jumpStart");
            } else if(Input.GetButtonDown("Jump") && Buffer() && rb.linearVelocity.y < 0)
            {
                jumpQueued = true;
            }
            // if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
            // {
            //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y *0.5f);
            // }
            
            if(IsGrounded())
            {
                animator.SetBool("isGrounded", true);
                animator.SetBool("isFalling", false);
                if (jumpQueued)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                    jumpQueued = false;
                    jumpAnimTime= 0.0f;
                    animator.SetTrigger("jumpStart");
                }
                
                airTime = 0.0f;

            }
            else
            {
                animator.SetBool("isGrounded", false);
            }
            
        }
        if(horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (airTime > 0.3f) 
        {
            animator.SetBool("isFalling", true);
        }


        flip();
        
        Debug.Log(jumpAnimTime);

        
    }
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     checkTags(collision);
    // }
    // private void checkTags(Collision2D collision)
    // {
    //     foreach (Transform child in collision.transform)
    //     {
    //         Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //         if (child.CompareTag("DamagePlayer") && iFrames <= 0)
    //         {
    //             playerHealth--;
    //             iFrames = 100;
    //         }
    //         if (child.CompareTag("KnockbackPlayer"))
    //         {
    //             Vector3 direction = -(collision.transform.position - transform.position).normalized;
    //             Vector3 force = direction * 10;
    //             // Vector2 contactPoint = collision.contacts[0].point;
    //             // Vector2 pushDirection = (Vector2)transform.position - contactPoint;
    //             rb.linearVelocity = Vector2.zero;
    //             playerKBTime = 0.4f;
    //             // rb.AddForce(pushDirection * 10, ForceMode2D.Impulse);
    //             rb.AddForce(force, ForceMode2D.Impulse);

    //         }

    //     }
    // }
    private bool IsGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0.0f, groundLayer);
        
    }
    private bool Buffer()
    {
        // return Physics2D.OverlapCircle(bufferCheck.position, 0.2f, groundLayer);
        return Physics2D.OverlapBox(bufferCheck.position, queueCheckSize, 0.0f, groundLayer);
    }
    private void FixedUpdate()
    {
        if(playerKBTime<=0)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }
    private void flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
}
