using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickerUnlock2 : IdlerUpgrade2
{
    public PlayerClicker playerClicker;
    //private Idler2 _idler;

    private void Start()
    {        
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText(playerClicker.ToString() + ":   " + UpgradeCost.ToString(), playerClicker.Description());
    }

    public void SetIdler(Idler2 idler)
    {
        _idler = idler;
        _image.sprite = _idler.IdlerObject.Icon;
    }

}
