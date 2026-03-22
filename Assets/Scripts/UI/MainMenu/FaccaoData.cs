using UnityEngine;

[CreateAssetMenu(fileName = "NovaFaccao", menuName = "Jogo/Facção")]
public class FaccaoData : ScriptableObject
{
    [Header("Informações")]
    public string nomeFaccao = "Humano";

    [TextArea(3, 6)]
    public string descricao = "Descrição da facção.";

    public Sprite imagem;

    [Header("Habilidades")]
    public HabilidadeData[] habilidades; // até 9
}