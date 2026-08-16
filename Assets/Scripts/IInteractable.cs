using UnityEngine;

public interface IInteractable
{
    void interact();
    void outline();
    void unoutline();
    bool canInteract();
}
