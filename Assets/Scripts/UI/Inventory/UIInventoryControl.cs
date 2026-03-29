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

    [HideInInspector] public int MaxSlots;

    void Awake()
    {
        pInventory = GetComponent<PlayerInventory>();
        pControl = GetComponent<PlayerControl>();
        pDamageSource = GetComponent<DamageSource>();
        pHealth = GetComponent<Health>();
        pStats = GetComponent<PlayerStats>();
        MaxSlots = pInventory.MaxSlots;
        UIInventory.SetInventoryControl(this);

        pInventory.OnInventoryChanged += updateUIInventory;
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
            UIInventory.HideInventory();

    }

    private void updateUIInventory() => UIInventory.UpdateUIInventory();
}
