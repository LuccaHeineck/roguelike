using System.Collections.Generic;
using UnityEngine;

/* 
 * ItemData é uma classe que representa os dados de um item no jogo. Ela é um ScriptableObject que pode ser criado como um asset no Unity.
 * Cada item tem um ID, nome, descrição, texto de efeito, sprite e listas de efeitos (ItemEffect) e efeitos de runtime (RuntimeItemEffect).
 * Os efeitos de item são usados para descrever o que o item faz em termos de mecânicas do jogo, enquanto os efeitos de runtime são usados para aplicar comportamentos específicos quando o item é adquirido pelo jogador.
 */

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
