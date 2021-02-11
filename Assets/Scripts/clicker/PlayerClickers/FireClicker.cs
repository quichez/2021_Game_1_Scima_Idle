using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireClicker : PlayerClicker, IPlayerClick
{
    public override string Description()
    {
        return "Clicks apply a burn that deals increasing damage every 15 Fire levels!";
    }

    public void PlayerClick()
    {
        if (Unlocked)
            StartCoroutine(Burn());
    }

    public override string ToString()
    {
        return "Fire Clicker";
    }

    IEnumerator Burn()
    {
        int i = 0;
        while(i<3)
        {
            //Stage.Instance.CurrentEnemy.TakeDamage(BaseDamage + 10 * _idler.IdlerObject.Level / 15);
            yield return new WaitForSeconds(1.0f);
            i++;
        }
    }
}
