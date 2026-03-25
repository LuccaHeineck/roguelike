using UnityEngine;

/* 
 * RuntimeItemEffect é uma classe base para efeitos de item que têm comportamento em tempo real no jogo, como efeitos que precisam ser atualizados a cada frame ou que reagem a eventos como causar dano.
 * Ela é um ScriptableObject que pode ser criado como um asset no Unity, e tem um método abstrato para criar uma instância do efeito em runtime (RuntimeItemEffectInstance).
 * 
 * A classe RuntimeItemEffectInstance define os métodos para aplicar, remover, atualizar a cada frame (OnTick) e reagir a eventos de hit (OnHitDealt) para o efeito em runtime.
 * Esses efeitos de runtime são usados para criar comportamentos dinâmicos e interativos para os itens no jogo, como buffs temporários, efeitos de área, etc.
 */

public abstract class RuntimeItemEffect : ScriptableObject
{
    public abstract RuntimeItemEffectInstance CreateInstance();
}

public abstract class RuntimeItemEffectInstance
{
    public virtual void OnApply(ItemEffectContext context) { }

    public virtual void OnRemove(ItemEffectContext context) { }

    public virtual void OnTick(ItemEffectContext context, float deltaTime) { }

    public virtual void OnHitDealt(ItemEffectContext context, HitEventData hitData) { }
}
