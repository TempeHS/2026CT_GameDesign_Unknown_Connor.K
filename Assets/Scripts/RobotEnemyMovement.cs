using UnityEngine;

public class RobotEnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDir = 1;


    private int curentDir;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    private bool isFacingRight = false;
    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
        curentDir = startDir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = speed * curentDir;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity =  movement;
        SetDir();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded=true;

        }
        else{
            isGrounded=false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        
        isGrounded=false;

    }
    private void SetDir()
    {
        if(!isGrounded) return;
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        if(rb.linearVelocity.x > 0){
            if(Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground"))){
            curentDir *=-1;
            // spriteRenderer.flipx = true;
            }
            else if(!Physics2D.Raycast(rightPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
                curentDir *=-1;
            }

        } 
        else if(rb.linearVelocity.x < 0){
            if(Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) ){
            curentDir *=-1;
            // spriteRenderer.flipx = false;
            }
            else if(!Physics2D.Raycast(leftPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
                curentDir *=-1;
            }
            
        }
        flip();

    }
     private void flip()
    {
        if ((isFacingRight && curentDir < 0f || !isFacingRight && curentDir > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
}
