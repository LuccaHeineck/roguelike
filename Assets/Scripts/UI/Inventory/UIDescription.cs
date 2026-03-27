using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDescription : MonoBehaviour
{
    [SerializeField] private GameObject itemImage;
    [SerializeField] private TMP_Text itemTitle;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemEffects;

    void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.SetActive(false);
        this.itemTitle.text = "";
        this.itemDescription.text = "";
        this.itemEffects.text = "";
    }
    public void SetDescription(UIInventoryItem item)
    {
        this.itemImage.SetActive(true);
        this.itemImage.GetComponent<Image>().sprite = item.image.sprite;
        this.itemTitle.text = item.title;
        this.itemDescription.text = item.description;
        this.itemEffects.text = item.effects;
    }

    public void SetDefaultDescription()
    {

    }
}
