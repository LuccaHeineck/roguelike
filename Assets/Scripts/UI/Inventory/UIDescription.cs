using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDescription : MonoBehaviour
{
    [SerializeField] private UIInventory UIInv;

    [SerializeField] private GameObject itemImage;
    [SerializeField] private TMP_Text itemTitle;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemEffects;

    public void ResetDescription()
    {
        this.itemImage.GetComponent<Image>().sprite = UIInv.defaultSlotSprite;
        this.itemTitle.text = "";
        this.itemDescription.text = "";
        this.itemEffects.text = "";
    }
    public void SetDescription(UIItem item)
    {
        this.itemImage.GetComponent<Image>().sprite = item.image.sprite;
        this.itemTitle.text = item.title;
        this.itemDescription.text = item.description;
        this.itemEffects.text = item.effects;
    }
}
