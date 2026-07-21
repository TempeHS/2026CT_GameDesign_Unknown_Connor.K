using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private float maxDashCD = 2.0f;
    private float playerDashCD;
    private float fillamount;

    [SerializeField] private Image DashFill;
    [SerializeField] private PlayerMovement Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        playerDashCD = Player.dashCD;
        //playerHealth = 8;
        //playerMaxHealth = 8;
        fillamount = playerDashCD / maxDashCD;


        DashFill.fillAmount = (1 - fillamount);


    }
}


