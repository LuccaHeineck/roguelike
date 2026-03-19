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
        //Debug.Log("Hit something: " + other.name);
        // Previne o personagem de se atacar
        if (other.transform.root == transform.root) return;

        if (!other.CompareTag("Hurtbox")) return;

        Health health = other.GetComponentInParent<Health>();

        if (health != null)
        {
            //Debug.Log("damage: " + damageSource.Damage);
            health.TakeDamage(damageSource.Damage);
        }
    }
}