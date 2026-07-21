using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void LateUpdate()
    {
        float offsetX = player.transform.position.x - transform.position.x;
        float offsetY = player.transform.position.y - transform.position.y;
        
        transform.Translate(Vector3.right * offsetX * Time.deltaTime*5);
        transform.Translate(Vector3.up * offsetY * Time.deltaTime *5);
        //if(transform.position.y < 2.0f)
        //{
        //    transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
        //}
    }
}
