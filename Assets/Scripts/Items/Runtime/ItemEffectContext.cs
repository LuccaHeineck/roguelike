using UnityEngine;

/* 
 * ItemEffectContext é uma classe que encapsula o contexto necessário para aplicar os efeitos de um item no jogador.
 * Ela contém referências ao GameObject do jogador, seu Transform, PlayerControl, PlayerStats e Health.
 * Essa classe é usada para fornecer acesso fácil a esses componentes para os efeitos de item em runtime, permitindo que eles modifiquem os stats do jogador, a saúde, ou interajam com o controle do jogador conforme necessário.
 */

public class ItemEffectContext
{
    public ItemEffectContext(GameObject owner)
    {
        Owner = owner;
        OwnerTransform = owner != null ? owner.transform : null;
        PlayerControl = owner != null ? owner.GetComponent<PlayerControl>() : null;
        PlayerStats = owner != null ? owner.GetComponent<PlayerStats>() : null;
        Health = owner != null ? owner.GetComponent<Health>() : null;
    }

    public GameObject Owner { get; }
    public Transform OwnerTransform { get; }
    public PlayerControl PlayerControl { get; }
    public PlayerStats PlayerStats { get; }
    public Health Health { get; }
}
