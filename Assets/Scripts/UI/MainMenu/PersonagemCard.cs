using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonagemCard : MonoBehaviour
{
    [Header("Referências do card")]
    public Image imgPersonagem;
    public TextMeshProUGUI txtNome;

    private PersonagemData dadosAtuais;
    private System.Action<int> onClicar; // callback genérico
    private int meuIndice;

    public void Preencher(PersonagemData dados, System.Action<int> callback, int indice)
    {
        dadosAtuais = dados;
        onClicar = callback;
        meuIndice = indice;

        gameObject.SetActive(true);
        txtNome.text = dados.nomePersonagem;

        if (dados.imagem != null)
            imgPersonagem.sprite = dados.imagem;
    }

    public void Limpar()
    {
        dadosAtuais = null;
        onClicar = null;
        gameObject.SetActive(false);
    }

    public void AoClicar()
    {
        onClicar?.Invoke(meuIndice);
    }
}