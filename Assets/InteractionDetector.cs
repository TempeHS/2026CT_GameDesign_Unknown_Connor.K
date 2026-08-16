using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{   
    public Transform player;
    public IInteractable interactableInRange = null; // closest interactable

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
        Debug.Log(interactableInRange);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent( out IInteractable interactable ) && interactable.canInteract())
        {
            interactableInRange?.unoutline();
            interactableInRange = interactable;
            interactableInRange?.outline();
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
