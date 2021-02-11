using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterClicker : PlayerClicker, IPlayerClick
{
    public override string Description()
    {
        return "Gain extra mana on click that increases every 15 water levels!";
    }

    public void PlayerClick()
    {
        if (Unlocked && !Stage.Instance.CurrentEnemy.IsDying)
            return;
        //Player.Instance.ChangeMana(BaseDamage+ 5 * _idler.IdlerObject.Level / 15);
    }

    public override string ToString()
    {
        return "Water Clicker";
    }
}
