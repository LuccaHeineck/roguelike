using UnityEngine;

[CreateAssetMenu(fileName = "NovaHabilidade", menuName = "Jogo/Habilidade")]
public class HabilidadeData : ScriptableObject
{
    public string nomeHabilidade;

    [TextArea(2, 4)]
    public string descricao;

    public Sprite icone;
}