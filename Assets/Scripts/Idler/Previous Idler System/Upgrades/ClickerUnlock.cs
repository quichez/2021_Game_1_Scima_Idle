using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickerUnlock : IdlerUpgrade
{
    public PlayerClicker playerClicker;

    private void Start()
    {
        GetComponent<Image>().sprite = Idler.IdlerObject.Icon;
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText(playerClicker.ToString() + ":   " + UpgradeCost.ToString(), playerClicker.Description());        
    }

    public override void UnlockUpgrade()
    {
        base.UnlockUpgrade();
        playerClicker.Unlock(true);
    }
}
