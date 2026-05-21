using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public float playerMaxHealth = 8.0f;
    public float playerHealth = 8.0f;
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

    }
}
