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
        PlayerControl = FindComponent<PlayerControl>(owner);
        PlayerStats = FindComponent<PlayerStats>(owner);
        Health = FindComponent<Health>(owner);
    }

    public GameObject Owner { get; }
    public Transform OwnerTransform { get; }
    public PlayerControl PlayerControl { get; }
    public PlayerStats PlayerStats { get; }
    public Health Health { get; }

    private static T FindComponent<T>(GameObject owner) where T : Component
    {
        if (owner == null)
            return null;

        return owner.GetComponent<T>()
            ?? owner.GetComponentInChildren<T>()
            ?? owner.GetComponentInParent<T>();
    }
}
