using UnityEngine;

public class HazardTagApplier : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        checkTags(collision);
    }
    private void checkTags(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            foreach (Transform child in transform)
            {
                Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
                if (child.CompareTag("DamagePlayer") && Player.iFrames <= 0)
                {
                    Player.playerHealth--;
                    Player.iFrames = 100;
                }
                if (child.CompareTag("KnockbackPlayer"))
                {
                    Vector2 contactPoint = collision.contacts[0].point;

                    Vector2 pushDirection = ((Vector2)Player.transform.position - contactPoint);
                    rb.linearVelocity = Vector2.zero;
                    Player.playerKBTime = 0.2f;
                    rb.AddForce(pushDirection * 25, ForceMode2D.Impulse);

                }

            }
        }
    }
}
