using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectController : MonoBehaviour
{
    [Header("Todas as facções — mesma lista do CharacterSelect")]
    public FaccaoData[] faccoes;

    [Header("UI — Header")]
    public TextMeshProUGUI txtNomeFaccao;

    [Header("UI — Cards de personagem (3 slots)")]
    public PersonagemCard[] cards;

    [Header("UI — Painel Direito")]
    public GameObject painelDireito;
    public Image imgPersonagem;
    public TextMeshProUGUI txtNomePersonagem;
    public TextMeshProUGUI txtDescricaoPersonagem;
    public Image imgArma;
    public TextMeshProUGUI txtNomeArma;
    public TextMeshProUGUI txtDescricaoArma;

    private FaccaoData faccaoAtual;
    private int personagemSelecionado = -1;

    void Start()
    {
        // Lê a facção que o jogador escolheu na tela anterior
        int indiceFaccao = PlayerPrefs.GetInt("FaccaoEscolhida", 0);

        if (indiceFaccao < faccoes.Length)
            faccaoAtual = faccoes[indiceFaccao];

        painelDireito.SetActive(false); // começa oculto
        AtualizarUI();
    }

    void AtualizarUI()
{
    if (faccaoAtual == null) return;

    txtNomeFaccao.text = faccaoAtual.nomeFaccao;

    for (int i = 0; i < cards.Length; i++)
    {
        if (i < faccaoAtual.personagens.Length && faccaoAtual.personagens[i] != null)
            cards[i].Preencher(faccaoAtual.personagens[i], SelecionarPersonagem, i);
        else
            cards[i].Limpar();
    }
}

    // Chamado pelo PersonagemCard ao ser clicado
    public void SelecionarPersonagem(int indice)
    {
        personagemSelecionado = indice;
        PersonagemData p = faccaoAtual.personagens[indice];

        painelDireito.SetActive(true);

        txtNomePersonagem.text = p.nomePersonagem;
        txtDescricaoPersonagem.text = p.descricao;
        txtNomeArma.text = p.nomeArma;
        txtDescricaoArma.text = p.descricaoArma;

        if (p.imagem != null)
            imgPersonagem.sprite = p.imagem;

        if (p.iconeArma != null)
            imgArma.sprite = p.iconeArma;
    }

    public void BotaoConfirmar()
    {
        if (personagemSelecionado == -1) return; // nenhum selecionado
        PlayerPrefs.SetInt("PersonagemEscolhido", personagemSelecionado);
        SceneManager.LoadScene("testeOne");
    }

    public void BotaoVoltar()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
}