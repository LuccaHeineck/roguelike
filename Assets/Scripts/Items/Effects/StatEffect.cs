using UnityEngine;

/* 
 * StatEffect é um tipo específico de ItemEffect que modifica um stat do jogador (definido por StatType) por um valor específico.
 * Ele implementa os métodos Apply e Remove para adicionar ou remover o bônus do stat ao PlayerStats do jogador.
 * Este efeito pode ser usado para criar itens que aumentam temporariamente a velocidade, saúde, dano, etc do jogador.
 */

[CreateAssetMenu(fileName = "NewStatEffect", menuName = "Game/Items/Effects/Stat")]
public class StatEffect : ItemEffect
{
    [SerializeField] private StatType stat;
    [SerializeField] private StatModifierType modifierType = StatModifierType.Flat;
    [SerializeField] private float value;

    public StatModifierType ModifierType => modifierType;

    public override void Apply(PlayerStats stats)
    {
        if (stats == null)
        {
            return;
        }

        stats.AddBonus(stat, modifierType, value);
    }

    public override void Remove(PlayerStats stats)
    {
        if (stats == null)
        {
            return;
        }

        stats.RemoveBonus(stat, modifierType, value);
    }
}
