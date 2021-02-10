using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostUpgrade : IdlerUpgrade
{
    public float costDecrease; 

    private void Start()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/cost_upgrade");

        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Cost Upgrade:   " + UpgradeCost.ToString(), "Reduces total cost of leveling up by " + (costDecrease-1.0f).ToString("P"));
    }

    public override void UnlockUpgrade()
    {
        base.UnlockUpgrade();
        Idler.ModifyTotalCost();
        Idler.UpdateLevelUpButton();
    }
}
