using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlerUpgradeGroup : MonoBehaviour
{
    private IdlerUpgrade[] Upgrades => GetComponentsInChildren<IdlerUpgrade>();

    //unused
    public void SetUnlockedUpgrades(IdlerObjectUpgrade[] idobUpgrades)
    {
        if (Upgrades.Length != idobUpgrades.Length)
            Debug.Log("Warning, upgrade group is different size than upgrade array");
        else
        {
            for (int i = 0; i < Upgrades.Length; i++)
            {
                if (idobUpgrades[i].unlocked)
                    Upgrades[i].UnlockUpgrade();
            }
        }
    }


}
