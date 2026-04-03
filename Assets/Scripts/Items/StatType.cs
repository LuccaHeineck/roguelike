/* 
 * StatType é um enum que define os tipos de stats que podem ser afetados por itens e efeitos no jogo.
 * Ele inclui tipos como velocidade de movimento, saúde máxima, defesa e dano, podemos adicionar mais.
 * Esses tipos são usados para identificar quais stats devem ser modificados quando um item ou efeito é aplicado ao jogador.
 */

public enum StatType
{
    MoveSpeed,
    MaxHealth,
    Defense,
    Damage
}