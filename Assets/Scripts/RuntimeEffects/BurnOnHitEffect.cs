using UnityEngine;

/* 
 * BurnOnHitEffect é uma classe que aplica o BurnEffect quando o player dá um hit
 * Ele seta os valores do burn e em seu "onHitDealt" atribui o BurnEffect no health da entidade e aplica o burn
 */

[CreateAssetMenu(fileName = "BurnOnHitEffect", menuName = "Game/Items/Runtime Effects/Burn On Hit")]
public class BurnOnHitEffect : RuntimeItemEffect
{
    [SerializeField, Range(0f, 1f)] private float procChance = 1f;
    [SerializeField, Min(1)] private int damagePerTick = 1;
    [SerializeField, Min(0.05f)] private float tickInterval = 1f;
    [SerializeField, Min(0.05f)] private float duration = 3f;
    [SerializeField, Min(1)] private int maxStacks = 5;
    [SerializeField, Min(1)] private int stacksPerProc = 1;

    public override RuntimeItemEffectInstance CreateInstance()
    {
        return new BurnOnHitEffectInstance(
            procChance,
            damagePerTick,
            tickInterval,
            duration,
            maxStacks,
            stacksPerProc);
    }

    private class BurnOnHitEffectInstance : RuntimeItemEffectInstance
    {
        private readonly float procChance;
        private readonly int damagePerTick;
        private readonly float tickInterval;
        private readonly float duration;
        private readonly int maxStacks;
        private readonly int stacksPerProc;

        public BurnOnHitEffectInstance(
            float procChance,
            int damagePerTick,
            float tickInterval,
            float duration,
            int maxStacks,
            int stacksPerProc)
        {
            this.procChance = Mathf.Clamp01(procChance);
            this.damagePerTick = Mathf.Max(1, damagePerTick);
            this.tickInterval = Mathf.Max(0.05f, tickInterval);
            this.duration = Mathf.Max(0.05f, duration);
            this.maxStacks = Mathf.Max(1, maxStacks);
            this.stacksPerProc = Mathf.Max(1, stacksPerProc);
        }

        public override void OnHitDealt(ItemEffectContext context, HitEventData hitData)
        {
            if (hitData.TargetHealth == null || hitData.TargetHealth.IsDead)
            {
                return;
            }

            if (Random.value > procChance)
            {
                return;
            }

            BurnEffect burnEffect = hitData.TargetHealth.GetComponent<BurnEffect>();
            if (burnEffect == null)
            {
                burnEffect = hitData.TargetHealth.gameObject.AddComponent<BurnEffect>();
            }

            burnEffect.ApplyOrRefreshBurn(
                damagePerTick,
                tickInterval,
                duration,
                maxStacks,
                stacksPerProc);
        }
    }
}