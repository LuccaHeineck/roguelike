// ItemSelectionScreen.cs
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ItemSelectionScreen : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject screenRoot;
    [SerializeField] private ItemCardUI[] cards;         // arrasta os 3 cards aqui
    [SerializeField] private PlayerInventory inventory;

    [Header("Pool de itens")]
    [SerializeField] private List<ItemData> itemPool;      // arrasta todos os ItemData aqui

    [Header("Debug Input")]
    [SerializeField] private Key openSelectionKey = Key.B;

    private const int DefaultCardCount = 3;

    private void Awake()
    {
        if (screenRoot == null)
        {
            Debug.LogError("[ItemSelectionScreen] screenRoot nao foi atribuido.");
            return;
        }

        // Se screenRoot for este mesmo objeto, desativar ele aqui mata o Update e o atalho B nunca dispara.
        if (screenRoot == gameObject)
        {
            Debug.LogWarning("[ItemSelectionScreen] screenRoot aponta para o mesmo objeto do script. Use um filho/painel como root para poder abrir com B.");
            return;
        }

        Close();
    }

    private void Update()
    {
        if (screenRoot != null && screenRoot.activeSelf)
        {
            return;
        }

        // botão temporário para abrir — depois você troca por outro gatilho
        if (Keyboard.current != null && Keyboard.current[openSelectionKey].wasPressedThisFrame)
            Open();
    }

    public void Open()
    {
        if (screenRoot == null)
        {
            Debug.LogWarning("[ItemSelectionScreen] screenRoot esta nulo.");
            return;
        }

        if (inventory == null)
        {
            inventory = FindFirstObjectByType<PlayerInventory>();
            if (inventory == null)
            {
                Debug.LogWarning("[ItemSelectionScreen] PlayerInventory nao encontrado para receber o item selecionado.");
                return;
            }
        }

        if (cards == null || cards.Length == 0)
        {
            Debug.LogWarning("[ItemSelectionScreen] Nenhum card foi atribuido.");
            return;
        }

        int cardCount = Mathf.Min(DefaultCardCount, cards.Length);

        if (itemPool == null || itemPool.Count < cardCount)
        {
            Debug.LogWarning($"Pool de itens precisa ter pelo menos {cardCount} itens.");
            return;
        }

        List<ItemData> selected = PickRandom(cardCount);

        for (int i = 0; i < cardCount; i++)
        {
            if (cards[i] == null)
            {
                Debug.LogWarning($"[ItemSelectionScreen] Card {i} esta nulo.");
                continue;
            }

            cards[i].Setup(selected[i], OnItemSelected);
        }

        screenRoot.SetActive(true);

        GlobalUIBlurController.SetBlurActive(this, true);
        GameManager.Instance.PauseGame();
    }

    private void OnItemSelected(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogWarning("[ItemSelectionScreen] Item selecionado veio nulo.");
            return;
        }

        if (inventory == null)
        {
            Debug.LogWarning("[ItemSelectionScreen] Inventory nulo ao selecionar item.");
            return;
        }

        inventory.TryAddItem(itemData, 1);
        Debug.Log($"[SELECIONADO] {itemData.ItemName}");

        Close();

        GlobalUIBlurController.SetBlurActive(this, false);
        GameManager.Instance.ResumeGame();
    }

    private List<ItemData> PickRandom(int count)
    {
        // copia a pool para não modificar a original
        List<ItemData> pool = new List<ItemData>(itemPool);
        List<ItemData> result = new List<ItemData>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index); // evita repetir o mesmo item
        }

        return result;
    }

    public void Close()
    {
        if (screenRoot != null)
        {
            screenRoot.SetActive(false);
        }
    }
}