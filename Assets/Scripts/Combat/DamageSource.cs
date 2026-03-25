using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private PlayerStats playerStats;

    public int Damage => Mathf.Max(1, damage + (playerStats != null ? playerStats.DamageBonus : 0));

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }
}