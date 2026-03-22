using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryDescription : MonoBehaviour
{
    [SerializeField] private GameObject itemImagePanel;
    [SerializeField] private TMP_Text itemTitlePanel;
    [SerializeField] private TMP_Text itemDescriptionPanel;
    [SerializeField] private TMP_Text itemEffectsPanel;


    void Awake()
    {
        ResetDescription();
    }


    public void ResetDescription()
    {
        this.itemImagePanel.SetActive(false);
        this.itemTitlePanel.text = "";
        this.itemDescriptionPanel.text = "";
        this.itemEffectsPanel.text = "";
    }
    public void SetDescription(Sprite sprite, string title, string description, string effects)
    {
        this.itemImagePanel.SetActive(true);
        this.itemImagePanel.GetComponent<Image>().sprite = sprite;
        this.itemTitlePanel.text = title;
        this.itemDescriptionPanel.text = description;
        this.itemEffectsPanel.text = effects;
    }
}

