using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{   
    public Transform player;
    public IInteractable interactableInRange = null;
    public GameObject interactableObject=null;
    public float interactableInRangeDist = 999999.9999f; // closest interactable

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.interact();
        }
    }

    // Update is called once per frame
    

    void Update()
    {
        if (player != null)
        {
            // Matches the exact position
            transform.position = player.position;

        }
        //Debug.Log(interactableInRange);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent( out IInteractable interactable ) && interactable.canInteract())
        {
            float collisionRange = Vector2.Distance(transform.position, collision.transform.position);
            if(interactableInRangeDist> collisionRange){
                interactableInRange?.unoutline();
                interactableInRange = interactable;
                interactableInRange?.outline();
                interactableObject=collision.gameObject;
                
            }
            interactableInRangeDist = Vector2.Distance(transform.position, interactableObject.transform.position);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange?.unoutline();
            interactableInRange = null;
            
        }
    }
}
