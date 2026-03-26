using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventoryControl : MonoBehaviour
{
    [SerializeField] private UIInventory UIInventory;
    [HideInInspector] public PlayerInventory pInventory;
    [HideInInspector] public PlayerControl pControl;
    [HideInInspector] public DamageSource pDamageSource;
    [HideInInspector] public Health pHealth;
    [HideInInspector] public PlayerStats pStats;
    private int MaxSlots;

    void Awake()
    {
        pInventory = GetComponent<PlayerInventory>();
        pControl = GetComponent<PlayerControl>();
        pDamageSource = GetComponent<DamageSource>();
        pHealth = GetComponent<Health>();
        pStats = GetComponent<PlayerStats>();
        MaxSlots = pInventory.MaxSlots;
        UIInventory.setInventoryControl(this);
    }

    void Start()
    {
        UIInventory.InitializeUIInventory(MaxSlots);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.isPressed)
        {
            if (!UIInventory.IsVisible())
                UIInventory.ShowInventory();
            return;
        }

        if (UIInventory.IsVisible())
        {
            UIInventory.HideInventory();
            UIInventory.UIDescription.ResetDescription();
        }
    }
}
