using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickerUnlock : IdlerUpgrade
{
    public PlayerClicker playerClicker;

    private void Start()
    {        
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText(playerClicker.ToString() + ":   " + UpgradeCost.ToString(), playerClicker.Description());
    }

    public void SetIdler(Idler idler)
    {
        _idler = idler;
        _image.sprite = _idler.IdlerObject.Icon;
    }

}
