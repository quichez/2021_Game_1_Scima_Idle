using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUpgrade2 : IdlerUpgrade2
{
    private void Start()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/mana_upgrade");
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Mana Upgrade:   " + UpgradeCost.ToString(), "Reduces total mana usage by " + (1.0f - amount).ToString("P"));
    }   
}
