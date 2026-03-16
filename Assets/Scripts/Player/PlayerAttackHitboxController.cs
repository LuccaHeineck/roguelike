using UnityEngine;

public class PlayerAttackHitboxController : MonoBehaviour
{
    [SerializeField] private Collider2D hitboxUp;
    [SerializeField] private Collider2D hitboxDown;
    [SerializeField] private Collider2D hitboxLeft;
    [SerializeField] private Collider2D hitboxRight;

    private Vector2 attackDirection;

    public void SetDirection(Vector2 direction)
    {
        attackDirection = direction;
    }

    private void DisableAll()
    {
        hitboxUp.enabled = false;
        hitboxDown.enabled = false;
        hitboxLeft.enabled = false;
        hitboxRight.enabled = false;
    }

    public void EnableHitbox()
    {
        DisableAll();

        // Debug.Log(Time.frameCount + " Hitbox ON");

        if (attackDirection.y > 0)
            hitboxUp.enabled = true;
        else if (attackDirection.y < 0)
            hitboxDown.enabled = true;
        else if (attackDirection.x < 0)
            hitboxLeft.enabled = true;
        else if (attackDirection.x > 0)
            hitboxRight.enabled = true;
    }

    public void DisableHitbox()
    {
        // Debug.Log(Time.frameCount + " Hitbox OFF");
        DisableAll();
    }
}