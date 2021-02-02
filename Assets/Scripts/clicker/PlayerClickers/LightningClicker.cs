using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningClicker : PlayerClicker, IPlayerClick
{
    public override string Description()
    {
        return "10% chance on click to do large damage, increases every 15 Lightning levels!";
    }

    public void PlayerClick()
    {
        if (Unlocked)
        {
            float roll = Random.value;
            if (roll < 0.1f)
                Stage.Instance.CurrentEnemy.TakeDamage(BaseDamage + 10 * _idler.IdlerObject.Level / 15);
        }
    }

    public override string ToString()
    {
        return "Lightning Clicker";
    }
}
