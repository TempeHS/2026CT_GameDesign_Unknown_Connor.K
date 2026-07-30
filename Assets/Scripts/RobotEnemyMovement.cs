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
        }
        Debug.DrawRay(transform.position, Vector2.right * (halfWidth * 0.1f), Color.red);

    }
}
