using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : IdlerUpgrade
{
    public float damageIncrease;

    private void Awake()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/damage_upgrade");
    }

    private void Start()
    {
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Damage Upgrade:   " + UpgradeCost.ToString(), "Increases total damage by " + (damageIncrease - 1.0f).ToString("P"));
    }

    public override void UnlockUpgrade()
    {
        base.UnlockUpgrade();
        Idler.ModifyTotalDamage();
    }
}
