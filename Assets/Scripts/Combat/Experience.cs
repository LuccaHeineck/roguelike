using UnityEngine;
using System;

public class Experience : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int xpBaseParaPrimeiroNivel = 100;
    [SerializeField] private float multiplicador = 1.4f;

    [Header("DEBUG (Read Only)")]
    [SerializeField] private int debugNivel;
    [SerializeField] private int debugXpAtual;
    [SerializeField] private int debugXpNecessario;

    public int Nivel { get; private set; } = 1;
    public int XpAtual { get; private set; } = 0;
    public int XpNecessario { get; private set; }

    // Outros sistemas assinam esse evento para reagir ao level up
    public event Action<int> OnLevelUp; // int = novo nível

    private void Awake()
    {
        XpNecessario = CalcularXpNecessario(Nivel);
        AtualizarDebug();
    }

    public void AddXP(int quantidade)
    {
        XpAtual += quantidade;

        // Verifica se subiu mais de um nível de uma vez
        while (XpAtual >= XpNecessario)
        {
            XpAtual -= XpNecessario;
            Nivel++;
            XpNecessario = CalcularXpNecessario(Nivel);
            OnLevelUp?.Invoke(Nivel);
        }

        AtualizarDebug();
    }

    // XP necessário cresce 1.4x a cada nível
    private int CalcularXpNecessario(int nivel)
    {
        return Mathf.RoundToInt(xpBaseParaPrimeiroNivel * Mathf.Pow(multiplicador, nivel - 1));
    }

    private void AtualizarDebug()
    {
        debugNivel = Nivel;
        debugXpAtual = XpAtual;
        debugXpNecessario = XpNecessario;
    }
}