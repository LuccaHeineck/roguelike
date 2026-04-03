using UnityEngine;

public interface IDamageProvider
{
    int GetDamage(int baseDamage);
}

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int baseDamage = 1;
    private IDamageProvider damageProvider;

    public int Damage => damageProvider != null
        ? Mathf.Max(1, damageProvider.GetDamage(baseDamage))
        : Mathf.Max(1, baseDamage);

    private void Awake()
    {
        damageProvider = GetComponentInParent<IDamageProvider>();
    }
}