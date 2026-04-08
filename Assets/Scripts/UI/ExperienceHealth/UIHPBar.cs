using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHPBar : MonoBehaviour
{
    [Header("Referências")]
    public PlayerStats playerStats;
    public Image hpBarFill;
    public TextMeshProUGUI txtHP;

    private void Awake()
    {
        if (playerStats != null) return;

        UIInventoryControl inventoryControl = FindFirstObjectByType<UIInventoryControl>();
        if (inventoryControl != null)
            playerStats = inventoryControl.pStats;

        if (playerStats == null)
            playerStats = FindFirstObjectByType<PlayerStats>();
    }

    private void OnEnable()
    {
        if (playerStats != null)
            playerStats.OnHealthChanged += OnHealthChanged;

        AtualizarBarra(); // atualiza ao abrir o inventário
    }

    private void OnDisable()
    {
        if (playerStats != null)
            playerStats.OnHealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        AtualizarBarra();
    }

    private void AtualizarBarra()
    {
        if (playerStats == null) return;

        int currentHealth = playerStats.CurrentHealth;
        int maxHealth = Mathf.Max(1, playerStats.CurrentMaxHealth);

        float progresso = (float)currentHealth / maxHealth;
        hpBarFill.fillAmount = progresso;

        if (txtHP != null)
            txtHP.text = $"{currentHealth}/{maxHealth}";
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        AtualizarBarra();
    }
}