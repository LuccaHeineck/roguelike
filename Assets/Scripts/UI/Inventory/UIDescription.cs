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
    public void SetDescription(Sprite sprite, string title, string description, string effects)
    {
        this.itemImage.SetActive(true);
        this.itemImage.GetComponent<Image>().sprite = sprite;
        this.itemTitle.text = title;
        this.itemDescription.text = description;
        this.itemEffects.text = effects;
    }
}
