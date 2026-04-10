/* 
 * ItemEffectApplier é uma classe estática responsável por aplicar e remover os efeitos de um item nos stats do jogador.
 * Ela tem métodos para aplicar ou remover um item inteiro ou uma pilha de itens, iterando sobre os efeitos do item e aplicando ou removendo cada um deles dos PlayerStats do jogador.
 * Além disso, ela interage com o ItemEffectController para adicionar ou remover os efeitos de runtime associados ao item.
 */

public static class ItemEffectApplier
{
    public static void ApplyItem(ItemData itemData, PlayerStats playerStats)
    {
        ApplyItemStack(itemData, playerStats, 1);
    }

    public static void ApplyItemStack(ItemData itemData, PlayerStats playerStats, int stackCount)
    {
        if (itemData == null || playerStats == null || stackCount <= 0)
        {
            return;
        }

        for (int stackIndex = 0; stackIndex < stackCount; stackIndex++)
        {
            foreach (ItemEffect effect in itemData.Effects)
            {
                if (effect == null)
                {
                    continue;
                }

                effect.Apply(playerStats);
            }
        }

        ItemEffectController runtimeController = ResolveOrCreateRuntimeController(playerStats);

        if (runtimeController != null)
        {
            runtimeController.AddItemEffects(itemData, stackCount);
        }
    }

    public static void RemoveItem(ItemData itemData, PlayerStats playerStats)
    {
        RemoveItemStack(itemData, playerStats, 1);
    }

    public static void RemoveItemStack(ItemData itemData, PlayerStats playerStats, int stackCount)
    {
        if (itemData == null || playerStats == null || stackCount <= 0)
        {
            return;
        }

        for (int stackIndex = 0; stackIndex < stackCount; stackIndex++)
        {
            foreach (ItemEffect effect in itemData.Effects)
            {
                if (effect == null)
                {
                    continue;
                }

                effect.Remove(playerStats);
            }
        }

        ItemEffectController runtimeController = ResolveOrCreateRuntimeController(playerStats);

        if (runtimeController != null)
        {
            runtimeController.RemoveItemEffects(itemData, stackCount);
        }
    }

    private static ItemEffectController ResolveOrCreateRuntimeController(PlayerStats playerStats)
    {
        if (playerStats == null)
            return null;

        ItemEffectController controller = playerStats.GetComponent<ItemEffectController>()
            ?? playerStats.GetComponentInParent<ItemEffectController>()
            ?? playerStats.GetComponentInChildren<ItemEffectController>();

        if (controller != null)
            return controller;

        return playerStats.transform.root.gameObject.AddComponent<ItemEffectController>();
    }
}
