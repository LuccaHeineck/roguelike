using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private DamageSource damageSource;

    private void Awake()
    {
        damageSource = GetComponentInParent<DamageSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Previne o personagem de se atacar
        if (other.transform.root == transform.root) return;

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            Debug.Log("damage: " + damageSource.Damage);
            health.TakeDamage(damageSource.Damage);
        }
    }
}