using UnityEngine;

public class PlayerLevelUp : MonoBehaviour
{
    [Header("Bônus por nível")]
    [SerializeField] private float bonusMaxHealth = 5f;
    [SerializeField] private float bonusDamage = 1f;
    [SerializeField] private float bonusDefense = 0.5f;
    [SerializeField] private float bonusMoveSpeed = 0.1f;

    private PlayerStats stats;
    private Health health;
    private Experience experience;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        health = GetComponent<Health>();
        experience = GetComponent<Experience>();
    }

    private void OnEnable()
    {
        experience.OnLevelUp += AplicarLevelUp;
    }

    private void OnDisable()
    {
        experience.OnLevelUp -= AplicarLevelUp;
    }

    private void AplicarLevelUp(int novoNivel)
    {
        // Aplica bônus flat em cada stat via PlayerStats
        stats.AddBonus(StatType.MaxHealth, StatModifierType.Flat, bonusMaxHealth);
        stats.AddBonus(StatType.Damage, StatModifierType.Flat, bonusDamage);
        stats.AddBonus(StatType.Defense, StatModifierType.Flat, bonusDefense);
        stats.AddBonus(StatType.MoveSpeed, StatModifierType.Flat, bonusMoveSpeed);

        // Restaura vida completa
        health.Heal(health.MaxHealth);

        Debug.Log($"Level Up! Nível {novoNivel}");
    }
}