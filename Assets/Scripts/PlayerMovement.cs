using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool jumpQueued = false;
    private bool isFacingRight = true;
    private Vector2 checkSize = new Vector2(0.90f , 1.0f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform bufferCheck;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    
    void Update()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        } else if(Input.GetButtonDown("Jump") && Buffer() && rb.linearVelocity.y < 0)
        {
            jumpQueued = true;
        }
        // if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        // {
        //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y *0.5f);
        // }
        
        if(IsGrounded() && jumpQueued)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpQueued = false;
        }
        flip(); 

        
    }
    private bool IsGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        return Physics2D.OverlapBox(groundCheck.position, checkSize, 0.0f, groundLayer);
    }
    private bool Buffer()
    {
        // return Physics2D.OverlapCircle(bufferCheck.position, 0.2f, groundLayer);
        return Physics2D.OverlapBox(bufferCheck.position, checkSize, 0.0f, groundLayer);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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
