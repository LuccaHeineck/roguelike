using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FactionSelectorController : MonoBehaviour
{
    [Header("Facções cadastradas")]
    public FaccaoData[] faccoes;

    [Header("UI — Header")]
    public TextMeshProUGUI txtFaccaoNome;

    [Header("UI — Painel Esquerdo")]
    public Image imgFaccao;
    public TextMeshProUGUI txtDescricao;

    [Header("UI — Habilidades")]
    public HabilidadeSlot[] habilidadeSlots;

    private int indiceAtual = 0;

    void Start()
    {
        AtualizarUI();
    }

    public void BotaoAnterior()
    {
        indiceAtual--;
        if (indiceAtual < 0)
            indiceAtual = faccoes.Length - 1;
        AtualizarUI();
    }

    public void BotaoProximo()
    {
        indiceAtual++;
        if (indiceAtual >= faccoes.Length)
            indiceAtual = 0;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (faccoes.Length == 0) return;

        FaccaoData faccao = faccoes[indiceAtual];

        txtFaccaoNome.text = faccao.nomeFaccao;
        txtDescricao.text = faccao.descricao;

        if (faccao.imagem != null)
            imgFaccao.sprite = faccao.imagem;

        for (int i = 0; i < habilidadeSlots.Length; i++)
        {
            if (i < faccao.habilidades.Length && faccao.habilidades[i] != null)
                habilidadeSlots[i].Preencher(faccao.habilidades[i]);
            else
                habilidadeSlots[i].Limpar();
        }
    }

    public void BotaoEscolher()
    {
        // Salva índice da facção para a próxima cena ler
        PlayerPrefs.SetInt("FaccaoEscolhida", indiceAtual);
        SceneManager.LoadScene("CharacterSelect");
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("MainMenu");
    }
}