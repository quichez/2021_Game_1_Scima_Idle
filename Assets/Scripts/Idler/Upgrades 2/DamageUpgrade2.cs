using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade2 : IdlerUpgrade2
{
    private void Start()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/damage_upgrade");
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Damage Upgrade:   " + UpgradeCost.ToString(), "Increases total damage by " + (amount - 1.0f).ToString("P"));
    }
}
