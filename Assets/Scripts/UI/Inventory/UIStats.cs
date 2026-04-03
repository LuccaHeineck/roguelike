using System;
using TMPro;
using UnityEngine;

public class UIStats : MonoBehaviour
{
    [SerializeField] private UIInventory UIInv;
    private PlayerStats pStats;
    [SerializeField] private TMP_Text leftList, rightList;

    private const string labelMaxHP = "Max hp: ";
    private const string labelHeal = "Heal: ";
    private const string labelDefense = "Defense: ";
    private const string labelMoveSpeed = "Move Speed: ";
    private const string labelDamage = "Damage: ";
    private const string labelAttackSpeed = "Attack Speed: ";
    private const string labelDoubleLF = "\n\n";

    private int maxHP = 0, heal = 0, defense = 0, damage = 0;
    private float moveSpeed = 0f, attackSpeed = 0f;

    void Start()
    {
        pStats = UIInv.UIInvControl.pStats;
    }

    public void SetStats()
    {
        if (!ensureStatsReference()) return;

        updateStats();

        setLeftList();
        setRightList();
    }

    private bool ensureStatsReference()
    {
        if (pStats != null) return true;

        // Tenta buscar a referência através da hierarquia que você já montou
        if (UIInv != null && UIInv.UIInvControl != null)
            pStats = UIInv.UIInvControl.pStats;

        return pStats != null;
    }

    private void updateStats()
    {
        maxHP = pStats.CurrentMaxHealth;
        heal = pStats.CurrentHeal;
        defense = pStats.CurrentDefense;
        damage = pStats.CurrentDamage;
        moveSpeed = pStats.CurrentMoveSpeed;
        attackSpeed = pStats.CurrentAttackSpeed;
    }

    private void setLeftList()
    {
        string leftListText = "";
        leftListText += labelMaxHP + maxHP + labelDoubleLF;

        leftListText += labelHeal + heal + labelDoubleLF;

        leftListText += labelDefense + defense + labelDoubleLF;

        leftListText += labelMoveSpeed + $"{moveSpeed:F2}";

        leftList.text = leftListText;
    }

    private void setRightList()
    {
        string rightListText = "";
        rightListText += labelDamage + damage + labelDoubleLF;

        rightListText += labelAttackSpeed + $"{attackSpeed:F2}" + labelDoubleLF;

        rightList.text = rightListText;
    }
}
