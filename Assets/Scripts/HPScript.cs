using UnityEngine;
using UnityEngine.UI;
public class HPScript : MonoBehaviour
{
    
    public int cloneID = 0;
    private float playerMaxHealth;
    private float playerHealth;
    private float fillamount;

    [SerializeField] private Image HealthPointFill;
    [SerializeField] private PlayerMovement Player;

    private void Awake()
    {
        
    

        playerMaxHealth = Player.playerMaxHealth / 2;
        playerHealth = Player.playerHealth / 2;


        if (cloneID > 0)
        {
            UpdateHealthBar();
        }
        
    
    }
    void Start()
    {
        playerMaxHealth = Player.playerMaxHealth / 2;
        playerHealth = Player.playerHealth / 2;

    }
    void Update()
    {

        playerMaxHealth = Player.playerMaxHealth / 2;
        playerHealth = Player.playerHealth / 2;


        if (cloneID > 0)
        {
            UpdateHealthBar();
        }
        
    }
    public void UpdateHealth(float amount)
    {
        playerMaxHealth = Player.playerMaxHealth/2;
        playerHealth = Player.playerHealth/2;

    }
    private void UpdateHealthBar()
    {
        //playerHealth = 8;
        //playerMaxHealth = 8;
        fillamount = playerHealth - (cloneID - 1.0f);
        if (fillamount > 1.0f)
        {
            fillamount = 1.0f;
        }else if(fillamount < 0.0f)
        {
            fillamount = 0.0f;
        }

        HealthPointFill.fillAmount = fillamount;



    }

    //void Update()
    //{
    //    Debug.Log(cloneID);
    //}
}
