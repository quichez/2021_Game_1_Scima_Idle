using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum IdlerName
{
    [EnumMember(Value = "Air")] Air,
    [EnumMember(Value = "Earth")] Earth,
    [EnumMember(Value = "Water")] Water,
    [EnumMember(Value = "Fire")] Fire,
    [EnumMember(Value = "Lightning")] Lightning,
    [EnumMember(Value = "Light")] Light,
    [EnumMember(Value = "Dark")] Dark
}

public enum IdlerStat
{
    [EnumMember(Value = "damage")] Damage,
    [EnumMember(Value = "mana")] Mana,
    [EnumMember(Value = "cost")] Cost
}

public class Idler : MonoBehaviour
{
    public IdlerObject IdlerObject;

    //public int Level => GetModifiedLevel();
    public BigNumber Damage;
    public BigNumber Mana => GetModifiedTotalMana().Rounded;
    public BigNumber Cost;

    private List<float> TotalDamageModifiers = new List<float>(20);
    private List<float> TotalManaModifiers = new List<float>(20);
    private List<float> TotalCostModifier = new List<float>(20);

    [Header("Idler Information")]
    public Image Icon;
    public TextMeshProUGUI IdlerName;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI ManaText;

    private IdlerUpgradeGroup IdlerUpgrades => GetComponentInChildren<IdlerUpgradeGroup>();
    private IdlerLevelUpButton _button => GetComponentInChildren<IdlerLevelUpButton>();

    private void Start()
    {
        int ind = transform.GetSiblingIndex();
        
        IdlerObject.Index = ind;
        SaveManager.TrackIdlerObject(IdlerObject);
        Icon.sprite = IdlerObject.Icon;
        Player.Instance.OnInventoryUpdateCallback += ModifyTotalDamage;
        Player.Instance.OnGoldUpdateCallback += ModifyTotalCost;
        Player.Instance.OnInventoryUpdateCallback += UpdateTextFields;        

        //Load The Game
        if(SaveManager.Instance?.Cache!=null)
        {            
            IdlerObject.Level = SaveManager.Instance.Cache.IdlerLevels[ind];
            IdlerObject.UpgradesUnlocked = SaveManager.Instance.Cache.UpgradesUnlocked[ind];
            IdlerUpgrades.OnLoad();
            ModifyTotalDamage();
            ModifyTotalCost();
            _button.UpdateButtonText();
        }
        else
        {
            IdlerObject.Level = 0;
            IdlerObject.ResetObject();
            ModifyTotalDamage();
            ModifyTotalCost();
            _button.UpdateButtonText();
        }        
    }

    public void ModifyTotalDamage()
    {
        //Start with initial values
        Damage = IdlerObject.BaseDamage;
        TotalDamageModifiers.Clear();

        //Add IdlerDamageUpgades to TotalDamageModifiers
        TotalDamageModifiers.AddRange(IdlerUpgrades?.GetIdlerDamageUpgrades());
        List<Equipment> playerEquipped = Player.Instance?.Inventory?.EquippedItems;

        //Wrapped in null check for OnEnable
        if(playerEquipped != null)
        {
            foreach (Equipment equipment in Player.Instance.Inventory.EquippedItems)
            {
                if(equipment.ID != -1)
                {
                    foreach (EquipmentStat stat in equipment.Stats)
                    {
                    
                       if (stat.Idler == (IdlerName)System.Enum.Parse(typeof(IdlerName), IdlerObject.name))
                       {
                            if (stat.Stat == IdlerStat.Damage)
                                TotalDamageModifiers.Add(stat.Amount);
                       }
                    }
                }
            }
        }
        if(TotalDamageModifiers != null)
        {
            foreach (float value in TotalDamageModifiers)
            {
                Damage *= value;
            }
        }
        BigNumber.RoundBigNumber(Damage);
        UpdateTextFields();
    }

    public BigNumber GetModifiedTotalMana()
    {
        TotalManaModifiers.Clear();
        BigNumber mana = IdlerObject.BaseMana;
        TotalManaModifiers = IdlerUpgrades?.GetIdlerManaUpgrades();
        if(TotalManaModifiers != null)
        {
            foreach (float value in TotalManaModifiers)
            {
                mana *= value;
            }
        }
        return mana;
    }

    public void ModifyTotalCost()
    {
        Cost = IdlerObject.BaseCost;
        TotalCostModifier.Clear();

        TotalCostModifier.AddRange(IdlerUpgrades?.GetIdlerCostUpgrades());

        foreach (Equipment equipment in Player.Instance.Inventory.EquippedItems)
        {
            if (equipment.ID != -1)
            {
                foreach (EquipmentStat stat in equipment.Stats)
                {

                    if (stat.Idler == (IdlerName)System.Enum.Parse(typeof(IdlerName), IdlerObject.name))
                    {
                        if (stat.Stat == IdlerStat.Cost)
                            TotalCostModifier.Add(stat.Amount);
                    }
                }
            }
        }
        if (TotalCostModifier != null)
        {
            foreach (float value in TotalCostModifier)
            {
                Cost *= value;
            }
        }
        BigNumber.RoundBigNumber(Cost);
        UpdateTextFields();
    }

    public void UpdateTextFields()
    {
        if(IdlerName != null && LevelText != null && DamageText != null && ManaText != null)
        {
            IdlerName.text = IdlerObject.name;
            LevelText.text = "Level " + IdlerObject.Level.ToString();
            DamageText.text = Damage.ToString() + " DPS";
            ManaText.text = Mana.ToString() + " MPS";
        }
    }


    public void ResetIdlerObject()
    {
        IdlerObject.ResetObject();
        ModifyTotalDamage();
        ModifyTotalCost();
        UpdateTextFields();
        _button.UpdateButtonText();
    }

    private void OnEnable()
    {
        ModifyTotalDamage();
    }
}
