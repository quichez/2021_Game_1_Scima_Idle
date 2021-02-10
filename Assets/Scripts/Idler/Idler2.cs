using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idler2
{
    public BigNumber Damage { get; private set; }
    public BigNumber Mana { get; private set; }
    public BigNumber Cost { get; private set; }

    public IdlerObject2 IdlerObject { get; }

    public delegate void OnLevelUp();
    public OnLevelUp OnLevelUpCallback;

    private List<float> _damageModifiers = new List<float>(20);
    private List<float> _manaModifiers = new List<float>(20);
    private List<float> _costModifiers = new List<float>(20);

    public Idler2(IdlerObject2 idob)
    {
        IdlerObject = idob;
        UpdateIdler();
        OnLevelUpCallback += UpdateIdler;

        Player.Instance.OnEquipCallback += UpdateIdler;
    }

    public void LevelUp(/*int amount*/)
    {
        IdlerObject.Level++; //+= amount;
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
}
