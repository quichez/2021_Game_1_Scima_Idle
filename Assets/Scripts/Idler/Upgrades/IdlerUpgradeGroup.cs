using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlerUpgradeGroup : MonoBehaviour
{
    private bool[] _upgradesUnlocked;// => GetComponentInParent<Idler>().IdlerObject.UpgradesUnlocked;
    public IdlerUpgrade[] Upgrades => GetComponentsInChildren<IdlerUpgrade>();

    private void Start()
    {
        _upgradesUnlocked = GetComponentInParent<Idler>().IdlerObject.UpgradesUnlocked;
        OnLoad();
    }

    public List<float> GetIdlerDamageUpgrades()
    {
        _upgradesUnlocked = GetComponentInParent<Idler>().IdlerObject.UpgradesUnlocked;
        List<float> values = new List<float>();        
        foreach (IdlerUpgrade item in Upgrades)
        {
            if(_upgradesUnlocked[item.transform.GetSiblingIndex()] && item.GetType() == typeof(DamageUpgrade))
                values.Add(((DamageUpgrade)item).damageIncrease);
        }
        return values;
    } 

    public List<float> GetIdlerManaUpgrades()
    {
        List<float> values = new List<float>();
        foreach (IdlerUpgrade item in Upgrades)
        {
            if (_upgradesUnlocked[item.transform.GetSiblingIndex()] && item.GetType() == typeof(ManaUpgrade))
                values.Add(1.0f/ ((ManaUpgrade)item).manaDecrease);
        }
        return values;
    }

    public List<float> GetIdlerCostUpgrades()
    {
        List<float> values = new List<float>();
        foreach (IdlerUpgrade item in Upgrades)
        {
            if (_upgradesUnlocked[item.transform.GetSiblingIndex()] && item.GetType() == typeof(CostUpgrade))
                values.Add(1.0f / ((CostUpgrade) item).costDecrease);
        }
        return values;
    }

    public bool GetClickerUnlock()
    {
        int i = 0;
        foreach (ClickerUnlock item in Upgrades)
        {
            i++;
        }
        if (i > 1)
        {
            Debug.Log("More than one Clicker Unlock in this group: " + name);
            return false;
        }
        else if (i == 0)
        {
            Debug.Log("No Clicker Unlock in this group: " + name);
            return false;
        }
        else
            return GetComponentInChildren<ClickerUnlock>().Unlocked;
    }

    public void OnLoad()
    {
        for (int i = 0; i < _upgradesUnlocked.Length; i++)
        {
            if (_upgradesUnlocked[i])
                Upgrades[i].UnlockUpgrade();
        }
    }
}
