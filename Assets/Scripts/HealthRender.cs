using Unity.VisualScripting;
using UnityEngine;

public class HealthRender : MonoBehaviour
{
    public GameObject healthPoint;
    public int cloneID = 1;
    public Transform canvas;
    
    [SerializeField] private PlayerMovement Player;

    private float playerMaxHealth;

    private float oldPlayerMaxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMaxHealth = Player.playerMaxHealth;
        remakeBars();

    }
    // Update is called once per frame

    void Update()
    {
        playerMaxHealth = Player.playerMaxHealth;
        if (playerMaxHealth != oldPlayerMaxHealth)
        {
            remakeBars();
            oldPlayerMaxHealth = playerMaxHealth;
        }
    }


    private void remakeBars() 
    {
        cloneID = 0;
        deleteOldBars();
        playerMaxHealth = Player.playerMaxHealth;
        for (int i = 0; i < Mathf.CeilToInt(playerMaxHealth/2); i++)
        {
            cloneID++;
            GameObject clone = Instantiate(healthPoint, canvas);
            clone.tag = "ToDelete";
            RectTransform rt = clone.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(((cloneID -1) * 45) + 50, -50);
            foreach (Transform child in clone.transform)
            {
                if (child.CompareTag("HealthFillUnscripted"))
                {
                    //child.gameObject.AddComponent<HPScript>();

                    child.gameObject.tag = "HealthFillScripted";
                    child.GetComponent<HPScript>().cloneID = cloneID;
                }
            }
            

        }
    }
    private void deleteOldBars()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("ToDelete");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
