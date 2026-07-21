using UnityEngine;

public class ParralaxController : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    public GameObject cam;
    public float parralaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosX=transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //vv- 0=move with cam / 1=wont move / 0.5=half 
        float distX = cam.transform.position.x * parralaxEffect;
        float distY = cam.transform.position.y * parralaxEffect;

        transform.position = new Vector3(startPosX + distX, startPosY + distY+(2*(1-parralaxEffect)),transform.position.z);
    }
}
