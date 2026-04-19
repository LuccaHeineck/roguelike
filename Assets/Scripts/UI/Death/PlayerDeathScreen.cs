using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeathScreen : MonoBehaviour
{
    [Header("Referências")]
    public Health playerHealth;
    public GameObject deathPanel;

    private void OnEnable()
    {
        playerHealth.OnDeath += MostrarTelaMorte;
    }

    private void OnDisable()
    {
        playerHealth.OnDeath -= MostrarTelaMorte;
    }

    private void MostrarTelaMorte()
    {
        // Ativa o blur
        GlobalUIBlurController.SetBlurActive(this, true);

        // Exibe o painel
        deathPanel.SetActive(true);

        // Pausa o jogo
        Time.timeScale = 0f;
    }

    public void BotaoVoltarMenu()
    {
        // Remove o blur
        GlobalUIBlurController.SetBlurActive(this, false);

        // Restaura o tempo
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}