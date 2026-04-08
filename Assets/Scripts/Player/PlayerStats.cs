using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * PlayerStats:
 * Responsável por armazenar modificadores (flat, percent, override)
 * e calcular o valor final de um stat com base em um valor base.
 */

public class PlayerStats : MonoBehaviour, IMaxHealthProvider, ICurrentHealthState, IDamageProvider, IDefenseProvider
{
    [Header("DEBUG (Read Only)")]
    [SerializeField] private int debugCurrentHealth;
    [SerializeField] private float debugMoveSpeed;
    [SerializeField] private int debugMaxHealth;
    [SerializeField] private int debugDefense;
    [SerializeField] private int debugDamage;
    [SerializeField] private float debugAttackSpeed;

    private readonly Dictionary<StatType, float> flatBonuses = new();
    private readonly Dictionary<StatType, float> percentBonuses = new();
    private readonly Dictionary<StatType, float> overrideValues = new();

    private PlayerControl playerControl;
    private Health healthComponent;

    public int CurrentMaxHealth { get; private set; }
    public int CurrentHeal { get; private set; }
    public int CurrentDefense { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public int CurrentDamage { get; private set; }
    public float CurrentAttackSpeed { get; private set; }
    public int CurrentHealth { get; private set; }

    int ICurrentHealthState.CurrentHealth
    {
        get => CurrentHealth;
        set => CurrentHealth = value;
    }

    // Events for stat changes
    public event Action<StatType, float> OnStatChanged;  // (stat, newValue)
    public event Action<int, int> OnHealthChanged;  // (currentHealth, maxHealth)

    private void Awake()
    {
        // temporario para testes - remover apenas ao implementar atualizacao destes stats
        CurrentHeal = 0;
        CurrentAttackSpeed = 0f;

        playerControl = GetComponent<PlayerControl>();
        healthComponent = GetComponent<Health>();

        if (healthComponent != null)
            healthComponent.OnHealthChanged += HandleHealthChanged;

        refreshCurrentStats();
    }

    private void OnDestroy()
    {
        if (healthComponent != null)
            healthComponent.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        debugCurrentHealth = currentHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
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

        RecalculateStat(stat);
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

        RecalculateStat(stat);
    }

    private void RecalculateStat(StatType stat)
    {
        float newValue = 0;
        switch (stat)
        {
            case StatType.MoveSpeed:
                CurrentMoveSpeed = Mathf.Max(0, (float)GetStat(StatType.MoveSpeed, playerControl.BaseMoveSpeed));
                newValue = CurrentMoveSpeed;
                break;
            case StatType.MaxHealth:
                CurrentMaxHealth = GetMaxHealth(playerControl.BaseMaxHealth);
                newValue = CurrentMaxHealth;

                if (healthComponent != null)
                    healthComponent.SyncCurrentHealthToMax();
                break;
            case StatType.Defense:
                CurrentDefense = GetDefense(playerControl.BaseDefense);
                newValue = CurrentDefense;
                break;
            case StatType.Damage:
                CurrentDamage = GetDamage(playerControl.BaseDamage);
                newValue = CurrentDamage;
                break;
            case StatType.AttackSpeed:
                CurrentAttackSpeed = GetAttackSpeed(playerControl.BaseAttackSpeed);
                newValue = CurrentAttackSpeed;
                break;
        }

        OnStatChanged?.Invoke(stat, newValue);
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

    public float GetAttackSpeed(float baseAttackSpeed)
    {
        float sourceBase = playerControl != null ? playerControl.BaseAttackSpeed : baseAttackSpeed;
        return Mathf.Max(0f, GetStat(StatType.AttackSpeed, sourceBase));
    }

    public float GetCurrentStat(StatType stat)
    {
        return stat switch
        {
            StatType.MoveSpeed => CurrentMoveSpeed,
            StatType.MaxHealth => CurrentMaxHealth,
            StatType.Damage => CurrentDamage,
            StatType.Defense => CurrentDefense,
            StatType.AttackSpeed => CurrentAttackSpeed,
            _ => 0f
        };
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
        debugAttackSpeed = CurrentAttackSpeed;
    }

    private void refreshCurrentStats()
    {
        float baseMoveSpeed = playerControl != null ? playerControl.BaseMoveSpeed : 0f;
        int baseMaxHealth = playerControl != null ? playerControl.BaseMaxHealth : 0;
        int baseDefense = playerControl != null ? playerControl.BaseDefense : 0;
        int baseDamage = playerControl != null ? playerControl.BaseDamage : 0;
        float baseAttackSpeed = playerControl != null ? playerControl.BaseAttackSpeed : 1f;

        CurrentMoveSpeed = GetStat(StatType.MoveSpeed, baseMoveSpeed);
        CurrentMaxHealth = Mathf.Max(1, Mathf.RoundToInt(GetStat(StatType.MaxHealth, baseMaxHealth)));
        CurrentDefense = GetDefense(baseDefense);
        CurrentDamage = GetDamage(baseDamage);
        CurrentAttackSpeed = GetAttackSpeed(baseAttackSpeed);

        if (healthComponent != null)
            healthComponent.SyncCurrentHealthToMax();

        debugMoveSpeed = CurrentMoveSpeed;
        debugCurrentHealth = CurrentHealth;
        debugMaxHealth = CurrentMaxHealth;
        debugDefense = CurrentDefense;
        debugDamage = CurrentDamage;
        debugAttackSpeed = CurrentAttackSpeed;
    }
}