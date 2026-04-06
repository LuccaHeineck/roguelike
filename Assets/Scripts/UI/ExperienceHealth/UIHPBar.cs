using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHPBar : MonoBehaviour
{
    [Header("Referências")]
    public Health health;
    public Image hpBarFill;
    public TextMeshProUGUI txtHP;

    private void OnEnable()
    {
        health.OnDamage += AtualizarBarra;
        health.OnDeath += AtualizarBarra;
        AtualizarBarra(); // atualiza ao abrir o inventário
    }

    private void OnDisable()
    {
        health.OnDamage -= AtualizarBarra;
        health.OnDeath -= AtualizarBarra;
    }

    private void Start()
    {
        AtualizarBarra();
    }

    private void AtualizarBarra()
    {
        if (health == null) return;

        float progresso = (float)health.CurrentHealth / health.MaxHealth;
        hpBarFill.fillAmount = progresso;

        if (txtHP != null)
            txtHP.text = $"{health.CurrentHealth}/{health.MaxHealth}";
    }
}