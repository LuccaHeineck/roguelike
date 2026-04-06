using UnityEngine;
using System;

public interface IMaxHealthProvider
{
    int GetMaxHealth(int baseMaxHealth);
}

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private IMaxHealthProvider maxHealthProvider;

    public int CurrentHealth;
    public int MaxHealth => maxHealthProvider != null
        ? Mathf.Max(1, maxHealthProvider.GetMaxHealth(maxHealth))
        : Mathf.Max(1, maxHealth);
    public bool IsDead { get; private set; }
    public event Action OnDeath;
    public event Action OnDamage;

    void Awake()
    {
        maxHealthProvider = GetComponent<IMaxHealthProvider>();
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;

        HitFlashShader flash = GetComponent<HitFlashShader>();
        if (flash != null)
            flash.Flash();

        if (CurrentHealth <= 0)
            Die();
        else
            Hurt();
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    private void Hurt()
    {
        OnDamage?.Invoke();
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();

        //Debug.Log($"{gameObject.name} died");
    }
}