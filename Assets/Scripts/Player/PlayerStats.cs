using UnityEngine;

/* 
 * PlayerStats é um componente que mantém bônus temporários de stats para o player.
 * ele permite adicionar e remover bônus para velocidade de movimento, saúde máxima, defesa e dano, etc -> StatType.
 * estes bônus podem ser aplicados por efeitos de itens e são usados para calcular os stats efetivos do jogador (não alterar os stats base do player).
 */

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float moveSpeedBonus;
    [SerializeField] private int maxHealthBonus;
    [SerializeField] private int defenseBonus;
    [SerializeField] private int damageBonus;

    public float MoveSpeedBonus => moveSpeedBonus;
    public int MaxHealthBonus => maxHealthBonus;
    public int DefenseBonus => defenseBonus;
    public int DamageBonus => damageBonus;

    public void AddBonus(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.MoveSpeed:
                moveSpeedBonus += value;
                break;
            case StatType.MaxHealth:
                maxHealthBonus += Mathf.RoundToInt(value);
                break;
            case StatType.Defense:
                defenseBonus += Mathf.RoundToInt(value);
                break;
            case StatType.Damage:
                damageBonus += Mathf.RoundToInt(value);
                break;
        }
    }

    public void RemoveBonus(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.MoveSpeed:
                moveSpeedBonus -= value;
                break;
            case StatType.MaxHealth:
                maxHealthBonus -= Mathf.RoundToInt(value);
                break;
            case StatType.Defense:
                defenseBonus -= Mathf.RoundToInt(value);
                break;
            case StatType.Damage:
                damageBonus -= Mathf.RoundToInt(value);
                break;
        }

        maxHealthBonus = Mathf.Max(0, maxHealthBonus);
        defenseBonus = Mathf.Max(0, defenseBonus);
        damageBonus = Mathf.Max(0, damageBonus);
    }
}
