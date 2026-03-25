using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private DamageSource damageSource;
    private ItemEffectController sourceEffectController;

    private void Awake()
    {
        damageSource = GetComponentInParent<DamageSource>();
        sourceEffectController = transform.root.GetComponent<ItemEffectController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit something: " + other.name);
        // Previne a entidade de se atacar
        if (other.transform.root == transform.root) return;

        if (!other.CompareTag("Hurtbox")) return;

        Health health = other.GetComponentInParent<Health>();

        if (health != null)
        {
            PlayerStats targetStats = health.GetComponent<PlayerStats>();
            int defense = targetStats != null ? targetStats.DefenseBonus : 0;
            int finalDamage = Mathf.Max(1, damageSource.Damage - defense);

            //Debug.Log("damage: " + damageSource.Damage);
            health.TakeDamage(finalDamage);

            if (sourceEffectController != null)
            {
                Vector2 hitPoint = other.ClosestPoint(transform.position);
                HitEventData hitEvent = new HitEventData(health, other, finalDamage, hitPoint);
                sourceEffectController.NotifyHitDealt(hitEvent);
            }
        }
    }
}