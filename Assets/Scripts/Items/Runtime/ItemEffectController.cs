using System.Collections.Generic;
using UnityEngine;

/* 
 * ItemEffectController é um componente que gerencia os efeitos de runtime dos itens adquiridos pelo jogador.
 * Ele mantém uma lista de efeitos ativos e suas instâncias, e é responsável por aplicar, remover e atualizar esses efeitos durante o jogo.
 * O controlador também pode notificar os efeitos ativos quando o jogador causa um hit, permitindo que eles reajam a esse evento.
 */

public class ItemEffectController : MonoBehaviour
{
    private class ActiveRuntimeEntry
    {
        public ItemData ItemData;
        public List<RuntimeItemEffectInstance> Instances;
    }

    private readonly List<ActiveRuntimeEntry> activeEntries = new List<ActiveRuntimeEntry>();
    private readonly List<RuntimeItemEffectInstance> activeInstances = new List<RuntimeItemEffectInstance>();

    private ItemEffectContext context;

    private void Awake()
    {
        context = new ItemEffectContext(gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < activeInstances.Count; i++)
        {
            activeInstances[i].OnTick(context, Time.deltaTime);
        }
    }

    public void AddItemEffects(ItemData itemData)
    {
        AddItemEffects(itemData, 1);
    }

    public void AddItemEffects(ItemData itemData, int stackCount)
    {
        if (itemData == null || itemData.RuntimeEffects == null || stackCount <= 0)
        {
            return;
        }

        for (int stackIndex = 0; stackIndex < stackCount; stackIndex++)
        {
            AddSingleItemEffects(itemData);
        }
    }

    private void AddSingleItemEffects(ItemData itemData)
    {
        if (itemData == null || itemData.RuntimeEffects == null)
        {
            return;
        }

        List<RuntimeItemEffectInstance> createdInstances = new List<RuntimeItemEffectInstance>();

        for (int i = 0; i < itemData.RuntimeEffects.Count; i++)
        {
            RuntimeItemEffect runtimeEffect = itemData.RuntimeEffects[i];
            if (runtimeEffect == null)
            {
                continue;
            }

            RuntimeItemEffectInstance instance = runtimeEffect.CreateInstance();
            if (instance == null)
            {
                continue;
            }

            instance.OnApply(context);
            createdInstances.Add(instance);
            activeInstances.Add(instance);
        }

        if (createdInstances.Count > 0)
        {
            activeEntries.Add(new ActiveRuntimeEntry
            {
                ItemData = itemData,
                Instances = createdInstances
            });
        }
    }

    public void RemoveItemEffects(ItemData itemData)
    {
        RemoveItemEffects(itemData, 1);
    }

    public void RemoveItemEffects(ItemData itemData, int stackCount)
    {
        if (itemData == null || stackCount <= 0)
        {
            return;
        }

        for (int stackIndex = 0; stackIndex < stackCount; stackIndex++)
        {
            bool removed = RemoveSingleItemEffects(itemData);
            if (!removed)
            {
                return;
            }
        }
    }

    private bool RemoveSingleItemEffects(ItemData itemData)
    {
        if (itemData == null)
        {
            return false;
        }

        for (int entryIndex = 0; entryIndex < activeEntries.Count; entryIndex++)
        {
            ActiveRuntimeEntry entry = activeEntries[entryIndex];
            if (entry.ItemData != itemData)
            {
                continue;
            }

            for (int i = 0; i < entry.Instances.Count; i++)
            {
                RuntimeItemEffectInstance instance = entry.Instances[i];
                instance.OnRemove(context);
                activeInstances.Remove(instance);
            }

            activeEntries.RemoveAt(entryIndex);
            return true;
        }

        return false;
    }

    public int GetRuntimeStackCount(ItemData itemData)
    {
        if (itemData == null)
        {
            return 0;
        }

        int count = 0;
        for (int i = 0; i < activeEntries.Count; i++)
        {
            if (activeEntries[i].ItemData == itemData)
            {
                count++;
            }
        }

        return count;
    }

    public void NotifyHitDealt(HitEventData hitData)
    {
        for (int i = 0; i < activeInstances.Count; i++)
        {
            activeInstances[i].OnHitDealt(context, hitData);
        }
    }
}
