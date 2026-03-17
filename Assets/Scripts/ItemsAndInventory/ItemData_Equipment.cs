using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,//武器
    Armor,//盔甲
    Amulet,//饰品
    Flask//药剂
}

[CreateAssetMenu(fileName = "New Item Data",menuName ="Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    [Header("Major Stats")]
    public int strength; //力量，影响物理伤害、暴击伤害
    public int agility; //敏捷，影响闪避、暴击率
    public int intelligence; //智力，影响法术伤害
    public int vitality; //体力，影响生命值

    [Header("Defensive Stats")]
    public int maxHealth;
    public int armor; //护甲，减少物理伤害
    public int MagicResistence; //魔抗，减少法术伤害
    public int evasion; //闪避，减少被命中概率

    [Header("Offensive Stats")]
    public int damage;
    public int CriticalChance;//暴击率
    public int CriticalPower; //暴击增伤150%

    [Header("Magic Stats")]
    public int FireDamage;
    public int IceDamage;
    public int LightningDamage;
    public void AddModifier()
    {
        PlayerStats playerStats=PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.MagicResistence.AddModifier(MagicResistence);
        playerStats.evasion.AddModifier(evasion);

        playerStats.damage.AddModifier(damage);
        playerStats.CriticalChance.AddModifier(CriticalChance);
        playerStats.CriticalPower.AddModifier(CriticalPower);

        playerStats.FireDamage.AddModifier(FireDamage);
        playerStats.IceDamage.AddModifier(IceDamage);
        playerStats.LightningDamage.AddModifier(LightningDamage);
    }

    public void RemoveModifier()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.MagicResistence.RemoveModifier(MagicResistence);
        playerStats.evasion.RemoveModifier(evasion);

        playerStats.damage.RemoveModifier(damage);
        playerStats.CriticalChance.RemoveModifier(CriticalChance);
        playerStats.CriticalPower.RemoveModifier(CriticalPower);

        playerStats.FireDamage.RemoveModifier(FireDamage);
        playerStats.IceDamage.RemoveModifier(IceDamage);
        playerStats.LightningDamage.RemoveModifier(LightningDamage);
    }
}


