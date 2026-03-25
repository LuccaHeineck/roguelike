using UnityEngine;

/* 
 * HitEventData é uma struct imutável que encapsula as informações relevantes sobre um evento de hit, como a saúde do alvo, o colisor atingido, o dano causado e o ponto de impacto.
 * Ela é usada para passar essas informações para os efeitos de item em runtime quando o jogador causa um hit, permitindo que eles reajam a esse evento de forma dinâmica.
 */

public readonly struct HitEventData
{
    public HitEventData(Health targetHealth, Collider2D targetCollider, int damageDealt, Vector2 hitPoint)
    {
        TargetHealth = targetHealth;
        TargetCollider = targetCollider;
        DamageDealt = damageDealt;
        HitPoint = hitPoint;
    }

    public Health TargetHealth { get; }
    public Collider2D TargetCollider { get; }
    public int DamageDealt { get; }
    public Vector2 HitPoint { get; }
}
