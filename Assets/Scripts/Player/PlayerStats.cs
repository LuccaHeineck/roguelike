using System.Collections.Generic;
using UnityEngine;

/*
 * PlayerStats:
 * Responsável por armazenar modificadores (flat, percent, override)
 * e calcular o valor final de um stat com base em um valor base.
 */

public class PlayerStats : MonoBehaviour, IMaxHealthProvider, IDamageProvider, IDefenseProvider
{
    [Header("DEBUG (Read Only)")]
    [SerializeField] private float debugMoveSpeed;
    [SerializeField] private int debugMaxHealth;
    [SerializeField] private int debugDefense;
    [SerializeField] private int debugDamage;

    private readonly Dictionary<StatType, float> flatBonuses = new();
    private readonly Dictionary<StatType, float> percentBonuses = new();
    private readonly Dictionary<StatType, float> overrideValues = new();

    private PlayerControl playerControl;

    public int CurrentMaxHealth { get; private set; }
    public int CurrentHeal { get; private set; }
    public int CurrentDefense { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public int CurrentDamage { get; private set; }
    public float CurrentAttackSpeed { get; private set; }

    private void Awake()
    {
        // temporario para testes - remover apenas ao implementar atualizacao destes stats
        CurrentHeal = 0;
        CurrentAttackSpeed = 0f;

        playerControl = GetComponent<PlayerControl>();
        refreshCurrentStats();
    }

    private void LateUpdate()
    {
        refreshCurrentStats();
    }

    public void AddBonus(StatType stat, StatModifierType type, float value)
    {
        if (type == StatModifierType.Override)
        {
            overrideValues[stat] = value;
        }
        else
        {
            applyStackBonus(stat, type, value);
        }

        refreshCurrentStats();
    }

    public void RemoveBonus(StatType stat, StatModifierType type, float value)
    {
        if (type == StatModifierType.Override)
        {
            overrideValues.Remove(stat);
        }
        else
        {
            applyStackBonus(stat, type, -value);
        }

        refreshCurrentStats();
    }

    public float GetStat(StatType stat, float baseValue)
    {
        if (overrideValues.TryGetValue(stat, out float overrideValue))
            return overrideValue;

        float flat = GetValue(flatBonuses, stat);
        float percent = GetValue(percentBonuses, stat);

        return (baseValue + flat) * (1f + percent);
    }

    private void applyStackBonus(StatType stat, StatModifierType type, float value)
    {
        switch (type)
        {
            case StatModifierType.Flat:
                flatBonuses[stat] = Mathf.Max(GetValue(flatBonuses, stat) + value, 0f);
                break;

            case StatModifierType.Percent:
                percentBonuses[stat] = Mathf.Max(GetValue(percentBonuses, stat) + value, 0f);
                break;
        }
    }

    private static float GetValue(Dictionary<StatType, float> dict, StatType stat)
    {
        return dict.TryGetValue(stat, out float value) ? value : 0f;
    }

    public int GetMaxHealth(int baseMaxHealth)
    {
        int sourceBase = playerControl != null ? playerControl.BaseMaxHealth : baseMaxHealth;
        return Mathf.Max(1, Mathf.RoundToInt(GetStat(StatType.MaxHealth, sourceBase)));
    }

    public int GetDamage(int baseDamage)
    {
        int sourceBase = playerControl != null ? playerControl.BaseDamage : baseDamage;
        return Mathf.Max(1, Mathf.RoundToInt(GetStat(StatType.Damage, sourceBase)));
    }

    public int GetDefense(int baseDefense)
    {
        int sourceBase = playerControl != null ? playerControl.BaseDefense : baseDefense;
        return Mathf.Max(0, Mathf.RoundToInt(GetStat(StatType.Defense, sourceBase)));
    }

    public void UpdateDebugStats(
        float baseMoveSpeed,
        int baseMaxHealth,
        int baseDefense,
        int baseDamage)
    {
        debugMoveSpeed = GetStat(StatType.MoveSpeed, baseMoveSpeed);
        debugMaxHealth = Mathf.RoundToInt(GetStat(StatType.MaxHealth, baseMaxHealth));
        debugDefense = Mathf.RoundToInt(GetStat(StatType.Defense, baseDefense));
        debugDamage = Mathf.RoundToInt(GetStat(StatType.Damage, baseDamage));
    }

    private void refreshCurrentStats()
    {
        float baseMoveSpeed = playerControl != null ? playerControl.BaseMoveSpeed : 0f;
        int baseMaxHealth = playerControl != null ? playerControl.BaseMaxHealth : 0;
        int baseDefense = playerControl != null ? playerControl.BaseDefense : 0;
        int baseDamage = playerControl != null ? playerControl.BaseDamage : 0;

        CurrentMoveSpeed = GetStat(StatType.MoveSpeed, baseMoveSpeed);
        CurrentMaxHealth = Mathf.Max(1, Mathf.RoundToInt(GetStat(StatType.MaxHealth, baseMaxHealth)));
        CurrentDefense = GetDefense(baseDefense);
        CurrentDamage = GetDamage(baseDamage);

        debugMoveSpeed = CurrentMoveSpeed;
        debugMaxHealth = CurrentMaxHealth;
        debugDefense = CurrentDefense;
        debugDamage = CurrentDamage;
    }
}