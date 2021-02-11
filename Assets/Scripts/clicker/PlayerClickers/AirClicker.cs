using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirClicker : PlayerClicker, IPlayerClick
{
    
    public void PlayerClick()
    {        
        if (Unlocked)
        {
            Debug.Log("Air Clicker Activated");
            //Stage.Instance.CurrentEnemy.TakeDamage(BaseDamage * (1 +_idler.IdlerObject.Level / 10));
        }
    }

    public override string ToString()
    {
        return "Air Clicker";
    }

    public override string Description()
    {
        return "Gain extra flat damage every ten Air levels!";
    }
}
