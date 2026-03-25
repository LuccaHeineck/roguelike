using UnityEngine;

/* 
 * ItemEffect é uma classe base para efeitos de itens no jogo. Ela é um ScriptableObject que pode ser criado como um asset no Unity.
 * Cada efeito de item tem uma descrição e métodos abstratos para aplicar e remover o efeito dos stats do jogador.
 * Os efeitos de item podem ser usados para criar itens que concedem bônus temporários ao jogador, como aumento de velocidade, saúde, dano, etc.
 */

public abstract class ItemEffect : ScriptableObject
{
    [TextArea(1, 3)]
    [SerializeField] private string effectDescription;

    public string EffectDescription => effectDescription;

    public abstract void Apply(PlayerStats stats);
    public abstract void Remove(PlayerStats stats);
}
