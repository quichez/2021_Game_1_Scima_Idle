using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IdlerObject2",menuName = "Idler/IdlerObject2")]
public class IdlerObject : ScriptableObject
{
    public IdlerName IdlerName;
    public int Level { get; set; }
    public int Index { get; set; }
    public Sprite Icon;

    #region BigNumber Members
    [Header("Base Damage")]
    [Range(0.0f, 9.999f)] public float DamageMantissa;
    public float DamageExponent;
    public BigNumber BaseDamage => GetModifiedBaseDamage() * Level;
    public BigNumber BaseDamagePerLevel { get { return GetModifiedBaseDamage(); } }

    [Header("Base Cost")]
    [Range(0.0f, 9.999f)] public float CostMantissa;
    public float CostExponent;
    public BigNumber BaseCost => GetModifiedBaseCost() * Mathf.Pow(CostScale, Level);

    [Header("Base Mana")]
    [Range(0.0f, 9.999f)] public float ManaMantissa;
    public float ManaExponent;
    public BigNumber BaseMana => GetModifiedBaseMana() * Level;
    public BigNumber BaseManaPerLevel { get { return GetModifiedBaseMana(); } }
    #endregion

    [Header("Cost Scaling")]
    [Range(1.0f, 2.0f)] public float CostScale;

    //IdlerObjectUpgrades are base modifiers and a clicker upgrade
    public IdlerObjectUpgrade[] idlerUpgrades = new IdlerObjectUpgrade[6];

    public List<float> BaseDamageModifiers { get; set; } = new List<float>();
    public List<float> BaseCostModifiers { get; set; } = new List<float>();
    public List<float> BaseManaModifiers { get; set; } = new List<float>();

    public BigNumber GetModifiedBaseDamage()
    {
        BigNumber baseDamage = new BigNumber(DamageMantissa, DamageExponent);
        foreach (IdlerObjectUpgrade idobUpgrade in idlerUpgrades)
        {
            if(idobUpgrade.type == IdlerUpgradeType.Damage && idobUpgrade.unlocked)                
                baseDamage *= idobUpgrade.amount;
        }        
        return baseDamage;
    }

    public BigNumber GetModifiedBaseMana()
    {
        BigNumber baseMana = new BigNumber(ManaMantissa, ManaExponent);
        foreach (IdlerObjectUpgrade idobUpgrade in idlerUpgrades)
        {
            if(idobUpgrade.type == IdlerUpgradeType.Mana && idobUpgrade.unlocked)
                baseMana *= idobUpgrade.amount;
        }
        return baseMana;
    }

    public BigNumber GetModifiedBaseCost()
    {
        BigNumber baseCost = new BigNumber(CostMantissa, CostExponent);
        foreach (IdlerObjectUpgrade idobUpgrade in idlerUpgrades)
        {
            if (idobUpgrade.type == IdlerUpgradeType.Cost && idobUpgrade.unlocked)
                baseCost *= idobUpgrade.amount;
        }
        return baseCost;
    }

    public PlayerClicker PlayerClicker;
}

[System.Serializable]
public class IdlerObjectUpgrade
{
    public IdlerUpgradeType type;
    public float amount;
    public bool unlocked;

    [Header("Upgrade Cost")]
    [Range(0.000f, 9.999f)] public double CostMantissa;
    public double CostExponent;
    public BigNumber UpgradeCost => new BigNumber(CostMantissa, CostExponent);
}


