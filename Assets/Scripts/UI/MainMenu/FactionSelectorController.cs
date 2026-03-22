using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectController : MonoBehaviour
{
    [Header("Facções cadastradas — adicione novas aqui")]
    public FaccaoData[] faccoes;

    [Header("UI — Header")]
    public TextMeshProUGUI txtFaccaoNome;   // texto "Humano" no topo

    [Header("UI — Painel Esquerdo")]
    public Image imgFaccao;                 // imagem da facção
    public TextMeshProUGUI txtDescricao;    // descrição da facção

    [Header("UI — Habilidades")]
    public HabilidadeSlot[] habilidadeSlots; // os 9 slots da grade

    private int indiceAtual = 0;            // qual facção está sendo exibida

    void Start()
    {
        AtualizarUI(); // exibe a primeira facção ao abrir a tela
    }

    // Chamado pelo BtnAnterior — volta uma facção na lista
    public void BotaoAnterior()
    {
        indiceAtual--;
        if (indiceAtual < 0)
            indiceAtual = faccoes.Length - 1; // se estava no primeiro, vai para o último
        AtualizarUI();
    }

    // Chamado pelo BtnProximo — avança uma facção na lista
    public void BotaoProximo()
    {
        indiceAtual++;
        if (indiceAtual >= faccoes.Length)
            indiceAtual = 0; // se estava no último, volta para o primeiro
        AtualizarUI();
    }

    // Atualiza todos os elementos visuais com os dados da facção atual
    void AtualizarUI()
    {
        if (faccoes.Length == 0) return;

        FaccaoData faccao = faccoes[indiceAtual];

        // Atualiza header
        txtFaccaoNome.text = faccao.nomeFaccao;

        // Atualiza painel esquerdo
        txtDescricao.text = faccao.descricao;
        if (faccao.imagem != null)
            imgFaccao.sprite = faccao.imagem;

        // Atualiza cada slot de habilidade
        // Slots sem habilidade correspondente são automaticamente ocultados
        for (int i = 0; i < habilidadeSlots.Length; i++)
        {
            if (i < faccao.habilidades.Length && faccao.habilidades[i] != null)
                habilidadeSlots[i].Preencher(faccao.habilidades[i]);
            else
                habilidadeSlots[i].Limpar();
        }
    }

    // Chamado pelo BtnEscolher — salva a escolha e vai para a próxima tela
    public void BotaoEscolher()
    {
        // Salva o índice escolhido para ser lido nas próximas cenas
        PlayerPrefs.SetInt("FaccaoEscolhida", indiceAtual);
        SceneManager.LoadScene("ChooseName");
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("MainMenu");
    }
}