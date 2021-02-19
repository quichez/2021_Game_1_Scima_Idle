using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public enum IdlerName
{
    [EnumMember(Value = "Air")] Air,
    [EnumMember(Value = "Earth")] Earth,
    [EnumMember(Value = "Water")] Water,
    [EnumMember(Value = "Fire")] Fire,
    [EnumMember(Value = "Lightning")] Lightning,
    [EnumMember(Value = "Light")] Light,
    [EnumMember(Value = "Dark")] Dark,
    [EnumMember(Value = "Fairy")] Fairy,
    [EnumMember(Value = "Spectral")] Spectral
}

public enum IdlerStat
{
    [EnumMember(Value = "damage")] Damage,
    [EnumMember(Value = "mana")] Mana,
    [EnumMember(Value = "cost")] Cost
}

public class Idler
{
    private PlayerIdlers _playerIdler;

    public BigNumber Damage { get; private set; }
    public BigNumber Mana { get; private set; }
    public BigNumber Cost { get; private set; }

    public IdlerObject IdlerObject { get; }

    public delegate void OnIdlerUpdate();
    public OnIdlerUpdate OnLevelUpCallback;
    public OnIdlerUpdate OnDamageUpdateCallback;

    private List<float> _damageModifiers = new List<float>(20);
    private List<float> _manaModifiers = new List<float>(20);
    private List<float> _costModifiers = new List<float>(20);

    private PlayerClicker2 _playerClicker;

    public Idler(IdlerObject idob, PlayerIdlers master)
    {
        _playerIdler = master;
        IdlerObject = idob;
        UpdateIdler();
        OnLevelUpCallback += UpdateIdler;
        OnLevelUpCallback += _playerIdler.UpdateTotalIdlerDamage;
        OnLevelUpCallback += _playerIdler.UpdateTotalIdlerManaCost;

        Player.Instance.OnEquipCallback += UpdateIdler;        
    }

    public void LevelUp(int amount=1)
    {
        IdlerObject.Level += amount;
        Player.Instance.ChangeGold(-Cost);
        OnLevelUpCallback?.Invoke();        
    }

    public void UpdateIdler()
    {
        Damage = ModifiedTotalDamage();
        Mana = IdlerObject.BaseMana;
        Cost = IdlerObject.BaseCost;        
    }

    public BigNumber ModifiedTotalDamage()
    {
        BigNumber dmg = IdlerObject.BaseDamage;
        _damageModifiers.Clear();
        foreach (Equipment equipment in Player.Instance.Inventory.EquippedItems)
        {
            if(equipment.ID != -1)
            {
                foreach (EquipmentStat stat in equipment.Stats)
                {
                    if (stat.Idler == IdlerObject.IdlerName)
                        dmg *= stat.Amount;
                }
            }
        }        
        return dmg.Rounded;
    }

    public BigNumber ModifiedTotalMana()
    {
        return BigNumber.Zero;
    }

    public BigNumber ModifiedTotalCost()
    {
        return BigNumber.Zero;
    }


    /// <summary>
    /// Load data for this class
    /// </summary>
    public void LoadData(int level = 0)
    {
        IdlerObject.Level = level;
        OnLevelUpCallback?.Invoke();
    }
}
