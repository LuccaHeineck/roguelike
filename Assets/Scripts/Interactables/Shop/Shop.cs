using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionPopUp;

    void Awake()
    {
        interactionPopUp.SetActive(false);
    }
    void Start()
    {

    }


    public void Interact()
    {
        if (CanInteract())
        {
            openShop();
        }
    }

    public bool CanInteract() => true;

    private void openShop()
    {
        Debug.Log("Abriu a loja!");
    }

    public void ActivatePopUp() => interactionPopUp.SetActive(true);
    public void DesactivatePopUp() => interactionPopUp.SetActive(false);

}
