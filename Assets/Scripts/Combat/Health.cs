using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private PlayerStats playerStats;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth + (playerStats != null ? playerStats.MaxHealthBonus : 0);
    public bool IsDead { get; private set; }
    public event Action OnDeath;
    public event Action OnDamage;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
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