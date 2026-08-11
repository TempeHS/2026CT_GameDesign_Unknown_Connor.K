using UnityEngine;

public class RobotEnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDir = 1;
    [SerializeField] private bool stayOnLedges = true;
    private Vector2 attackCheckSize = new Vector2(1.5f, 1.4f);


    private int curentDir;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    private bool isFacingRight = false;
    private bool isGrounded;
    private bool seePlayer;
    private float atkChargeTime;
    public GameObject robotAttackBox;
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
        atkChargeTime -= Time.deltaTime;
        if (atkChargeTime < 0)
        {
            movement.x = speed * curentDir;
        }
        else
        {
            movement.x = 0.0f;
        }
        
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity =  movement;
        SetDir();
        if (atkChargeTime<0.2f && atkChargeTime >0.1f)
        {
            robotAttackBox.SetActive(true);
        }
        else
        {
            robotAttackBox.SetActive(false);
        }
        
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
        if (atkChargeTime>0)
        {
            return;
        }
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;


        if (CheckEnemies())
        {
            if(transform.position.x > Player.transform.position.x)
            {
                curentDir = -1;
                atkChargeTime = 0.7f;
                animator.SetTrigger("attack");
            }
            if (transform.position.x < Player.transform.position.x)
            {
                curentDir = 1;
                atkChargeTime = 0.7f;
                animator.SetTrigger("attack");
            }
            stayOnLedges = false;

        }
        else
        {
            stayOnLedges = true;
        }
        if (rb.linearVelocity.x > 0){

            //if (Physics2D.Raycast(transform.position, Vector2.right, 0.7f, LayerMask.GetMask("Player")))
            //{
            //    atkChargeTime = 0.7f;
            //    animator.SetTrigger("attack");

            //}
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground"))){
            curentDir *=-1;
            // spriteRenderer.flipx = true;
            }
            else if(stayOnLedges && !Physics2D.Raycast(rightPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
                curentDir *=-1;
            }
            

        } 
        else if(rb.linearVelocity.x < 0){

            //if (Physics2D.Raycast(transform.position, Vector2.left, 0.7f, LayerMask.GetMask("Player")))
            //{
            //    atkChargeTime = 0.7f;
            //    animator.SetTrigger("attack");
            //}
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) ){
            curentDir *=-1;
            // spriteRenderer.flipx = false;
            }
            else if(stayOnLedges && !Physics2D.Raycast(leftPos, Vector2.down, halfHeight +0.1f, LayerMask.GetMask("Ground"))){
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
    private bool CheckEnemies()
    {
        return Physics2D.OverlapBox(transform.position, attackCheckSize, 0.0f, LayerMask.GetMask("Player"));
    }
}
