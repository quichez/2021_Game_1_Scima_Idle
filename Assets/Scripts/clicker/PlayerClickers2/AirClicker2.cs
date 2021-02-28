using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerClickers;

public class AirClicker2 : PlayerClicker2
{
    public override string ClickerName => "Air Clicker";
    public override string ClickerType => "Air";
    public override string Description => "Deal extra flat damage that increases every 10 air levels.";

    public BigNumber BaseDamage = new BigNumber(100);
    
    public override BigNumber Click(int level)
    {
        Activated = true;
        BigNumber dmg = BaseDamage * (1 + level / 10);
        Stage.Instance.CurrentEnemy.TakeDamage(dmg);
        return dmg;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 0.75f);
    }
}
