using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemId;
    [SerializeField] private string itemName;

    [TextArea(2, 5)]
    [SerializeField] private string itemDescription;

    [TextArea(2, 5)]
    [SerializeField] private string itemEffectText;

    [SerializeField] private Sprite itemSprite;
    [SerializeField] private List<ItemEffect> effects = new List<ItemEffect>();
    [SerializeField] private List<RuntimeItemEffect> runtimeEffects = new List<RuntimeItemEffect>();

    public string ItemId => itemId;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public string ItemEffectText => itemEffectText;
    public Sprite ItemSprite => itemSprite;
    public IReadOnlyList<ItemEffect> Effects => effects;
    public IReadOnlyList<RuntimeItemEffect> RuntimeEffects => runtimeEffects;
}
