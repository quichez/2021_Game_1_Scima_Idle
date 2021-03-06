using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickerUnlock : IdlerUpgrade
{
    private void Start()
    {        
        _tooltipTrigger.EnableToolTip(true);
    }

    public void SetIdler(Idler idler)
    {
        _idler = idler;
        _image.sprite = _idler.IdlerObject.Icon;
        //maybe for variety sake, use a foreach to find the clicker upgrade in the IdlerObject's upgrade list.
        _tooltipTrigger.SetToolTipText(idler._playerClicker.ClickerName + ": " + idler.IdlerObject.idlerUpgrades[0].UpgradeCost.ToString(), idler._playerClicker.Description);
    }

}
