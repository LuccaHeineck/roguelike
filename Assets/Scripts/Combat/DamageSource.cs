using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int Damage => damage;
}