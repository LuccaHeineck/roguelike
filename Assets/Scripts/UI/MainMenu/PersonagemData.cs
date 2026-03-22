using UnityEngine;

[CreateAssetMenu(fileName = "NovoPersonagem", menuName = "Jogo/Personagem")]
public class PersonagemData : ScriptableObject
{
    [Header("Informações")]
    public string nomePersonagem;

    [TextArea(3, 6)]
    public string descricao;

    public Sprite imagem;

    [Header("Arma / Habilidade")]
    public string nomeArma;

    [TextArea(2, 4)]
    public string descricaoArma;

    public Sprite iconeArma;
}