using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIXPBar : MonoBehaviour
{
    [Header("Referências")]
    public Experience experience;
    public Image xpBarFill;
    public TextMeshProUGUI txtXP;
    public TextMeshProUGUI txtNivel;

    private void OnEnable()
    {
        experience.OnLevelUp += AtualizarNivel;
    }

    private void OnDisable()
    {
        experience.OnLevelUp -= AtualizarNivel;
    }

    private void Update()
    {
        AtualizarBarra();
    }

    private void AtualizarBarra()
    {
        if (experience == null) return;

        float progresso = (float)experience.XpAtual / experience.XpNecessario;
        xpBarFill.fillAmount = progresso;

        txtXP.text = $"{experience.XpAtual}/{experience.XpNecessario}";
    }


    public void AtualizarNivel(int novoNivel)
    {
        txtNivel.text = $"{novoNivel}";
    }

    private void Start()
    {
        txtNivel.text = $"{experience.Nivel}";
        AtualizarBarra();
    }
}