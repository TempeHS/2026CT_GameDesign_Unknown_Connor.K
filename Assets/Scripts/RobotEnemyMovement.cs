using UnityEngine;

public class RobotEnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDir = 1;

    private int curentDir;
    private float halfWidth;
    private Vector2 movement;
    private bool isFacingRight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
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
    private void SetDir()
    {
        if(Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rb.linearVelocity.x > 0){
            curentDir *=-1;
            // spriteRenderer.flipx = true;
        }
        else if(Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rb.linearVelocity.x < 0){
            curentDir *=-1;
            // spriteRenderer.flipx = false;
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
