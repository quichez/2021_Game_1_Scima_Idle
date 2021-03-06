using UnityEngine;
using PlayerClickers;

public class DarkClicker2 : PlayerClicker
{
    public override string ClickerName => "Dark Clicker";
    public override string ClickerType => "Dark";
    public override string Description => "Deal extra current health damage on click that increases every 20 dark levels.";

    public override BigNumber Click(int level)
    {
        Activated = true;
        return new BigNumber(1000) * (1 + level/20) * Stage.Instance.CurrentEnemy.HealthPercentage;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 1.0f);
    }
}