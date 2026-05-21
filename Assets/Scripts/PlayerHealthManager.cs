using TMPro;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float playerMaxHealth = 8.0f;
    public float playerHealth = 8.0f;
    private int iFrames = 0;

    [SerializeField] private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerMaxHealth = 8.0f;
        playerHealth = 8.0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        checkTags(collision);
    }

    private void checkTags(Collision2D collision)
    {
        foreach (Transform child in collision.transform)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (child.CompareTag("DamagePlayer") && iFrames<=0)
            {
                playerHealth--;
                iFrames = 50;
            }
            if (child.CompareTag("KnockbackPlayer"))
            {
                

                

                //Vector2 pushDirection = new Vector2(direction, 0.5f).normalized;
                //rb.linearVelocity = Vector2.zero;
                //rb.AddForce(pushDirection * 100, ForceMode2D.Impulse);
                //rb.AddForce(pushDirection * 10f, ForceMode2D.Impulse);
                //Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                //rb.AddForce(pushDirection * 10, ForceMode2D.Impulse);

                //Vector2 direction = collision.transform.position - transform.position;
                //Vector2 normalizedDirection = direction.normalized;
                

                Vector2 contactPoint = collision.contacts[0].point;
                Vector2 pushDirection = (Vector2)transform.position - contactPoint;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(pushDirection * 10, ForceMode2D.Impulse);

            }

        }
    }
}
