using UnityEngine;

public class SlimeAttackHitboxController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D attackHitbox;

    private void EnableHitbox() => attackHitbox.enabled = true;
    private void DisableHitbox() => attackHitbox.enabled = false;

}

