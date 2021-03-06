using UnityEngine;
using System.Collections;
using PlayerClickers;

public class LightClicker2 : PlayerClicker
{
    public override string ClickerName => "Light Clicker";

    public override string ClickerType => "Light";

    public override string Description => "Deal extra missing health damage that increases every 20 light levels.";

    public override BigNumber Click(int level)
    {
        Activated = true;
        return new BigNumber(10000) * (1 + level / 20) * (1.0f - Stage.Instance.CurrentEnemy.HealthPercentage);
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 0.5f);
    }
}
