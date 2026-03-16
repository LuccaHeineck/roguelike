using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += Die;
    }

    private void OnDisable()
    {
        health.OnDeath -= Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}