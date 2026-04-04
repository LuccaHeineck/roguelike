using UnityEngine;

public class BurnEffect : MonoBehaviour
{
	[SerializeField] private bool showDebugLogs;

	private Health health;

	private int damagePerTick;
	private float tickInterval;
	private float remainingDuration;
	private float tickTimer;
	private int currentStacks;
	private int maxStacks;

	public int CurrentStacks => currentStacks;
	public bool IsActive => currentStacks > 0 && remainingDuration > 0f;

	private void Awake()
	{
		health = GetComponent<Health>();
		enabled = false;
	}

	private void Update()
	{
		if (health == null || health.IsDead)
		{
			Clear();
			return;
		}

		if (!IsActive)
		{
			enabled = false;
			return;
		}

		remainingDuration -= Time.deltaTime;
		if (remainingDuration <= 0f)
		{
			Clear();
			return;
		}

		tickTimer -= Time.deltaTime;
		while (tickTimer <= 0f && IsActive)
		{
			int totalTickDamage = Mathf.Max(1, damagePerTick * currentStacks);
			health.TakeDamage(totalTickDamage);

			if (showDebugLogs)
			{
				Debug.Log($"Burn tick on {name}: {totalTickDamage} damage ({currentStacks} stacks)");
			}

			tickTimer += tickInterval;
		}
	}

	public void ApplyOrRefreshBurn(
		int newDamagePerTick,
		float newTickInterval,
		float newDuration,
		int newMaxStacks,
		int addedStacks)
	{
		if (health == null)
		{
			health = GetComponent<Health>();
		}

		if (health == null || health.IsDead)
		{
			return;
		}

		damagePerTick = Mathf.Max(1, newDamagePerTick);
		tickInterval = Mathf.Max(0.05f, newTickInterval);
		maxStacks = Mathf.Max(1, newMaxStacks);

		int stackIncrease = Mathf.Max(1, addedStacks);
		currentStacks = Mathf.Clamp(currentStacks + stackIncrease, 1, maxStacks);
		remainingDuration = Mathf.Max(0.05f, newDuration);

		if (!enabled)
		{
			tickTimer = tickInterval;
			enabled = true;
		}
	}

	private void Clear()
	{
		currentStacks = 0;
		remainingDuration = 0f;
		tickTimer = 0f;
		enabled = false;
	}
}
