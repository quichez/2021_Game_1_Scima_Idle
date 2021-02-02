using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUpgrade : IdlerUpgrade
{
    public float manaDecrease;

    private void Awake()
    {
        _image.sprite = Resources.Load<Sprite>("Sprites/mana_upgrade");        
    }

    private void Start()
    {
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("Mana Upgrade:   " + UpgradeCost.ToString(), "Reduces total mana usage by " + (manaDecrease - 1.0f).ToString("P"));
    }

    public override void UnlockUpgrade()
    {
        base.UnlockUpgrade();
        Idler.GetModifiedTotalMana();
    }
}
