using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaHabilidade", menuName = "Jogo/Habilidade")]
public class HabilidadeData : ScriptableObject
{
    public string nomeHabilidade;

    [TextArea(2, 4)]
    public string descricao;

    public Sprite icone;

    [Header ("Estado bloqueado")]
    public bool isLocked = false; 

    [TextArea(2, 4)]
    public string descricaoLocked;

    public Sprite iconeLocked;

}