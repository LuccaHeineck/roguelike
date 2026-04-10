using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    IInteractable closestInteractableInRange = null;

    public void OnInteract()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
            closestInteractableInRange?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            closestInteractableInRange = interactable;
            closestInteractableInRange.ActivatePopUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == closestInteractableInRange)
        {
            closestInteractableInRange.DesactivatePopUp();
            closestInteractableInRange = null;
        }
    }
}
