using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostUpgrade2 : IdlerUpgrade2
{
    private void Start()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/cost_upgrade");

        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Cost Upgrade:   " + UpgradeCost.ToString(), "Reduces total cost of leveling up by " + (1.0f - amount).ToString("P"));
    }   
}
