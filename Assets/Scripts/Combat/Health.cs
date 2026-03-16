using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }
    public event Action OnDeath;

    void Awake()
    {
        CurrentHealth = maxHealth;
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
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();

        Debug.Log($"{gameObject.name} died");
    }
}