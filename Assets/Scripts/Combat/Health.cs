using UnityEngine;
using System;

public interface IMaxHealthProvider
{
    int GetMaxHealth(int baseMaxHealth);
}

public interface ICurrentHealthState
{
    int CurrentHealth { get; set; }
}

public class Health : MonoBehaviour
{
    [Header("DEBUG (Read Only)")]
    [SerializeField] private int debugCurrentHealth;
    [SerializeField] private int debugMaxHealth;

    private IMaxHealthProvider maxHealthProvider;
    private ICurrentHealthState currentHealthState;
    private int maxHealth = 10;
    private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealthState != null ? currentHealthState.CurrentHealth : currentHealth;
        private set
        {
            if (currentHealthState != null)
                currentHealthState.CurrentHealth = value;
            else
                currentHealth = value;

            debugCurrentHealth = value;
            debugMaxHealth = MaxHealth;
        }
    }
    public int MaxHealth => maxHealthProvider != null
        ? Mathf.Max(1, maxHealthProvider.GetMaxHealth(maxHealth))
        : Mathf.Max(1, maxHealth);
    public bool IsDead { get; private set; }
    public event Action OnDeath;
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action<int, int> OnHealthChanged;  // (currentHealth, maxHealth)

    void Awake()
    {
        maxHealthProvider = GetComponent<IMaxHealthProvider>();
        currentHealthState = GetComponent<ICurrentHealthState>();
        CurrentHealth = MaxHealth;
        UpdateDebugValues();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        UpdateDebugValues();

        OnDamage?.Invoke();

        HitFlashShader flash = GetComponent<HitFlashShader>();
        if (flash != null)
            flash.Flash();

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        UpdateDebugValues();
        OnHeal?.Invoke();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void SyncCurrentHealthToMax()
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateDebugValues();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void UpdateDebugValues()
    {
        debugCurrentHealth = CurrentHealth;
        debugMaxHealth = MaxHealth;
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();

        //Debug.Log($"{gameObject.name} died");
    }
}