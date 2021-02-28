using UnityEngine;
using PlayerClickers;

public class DarkClicker2 : PlayerClicker2
{
    public override string ClickerName => "Dark Clicker";

    public override string ClickerType => "Dark";

    public override string Description => "Deal extra current health damage on click that increases every 20 dark levels.";

    public override BigNumber Click(int level)
    {
        throw new System.NotImplementedException();
    }

    public override void DelayedDestroy(GameObject effect)
    {
        throw new System.NotImplementedException();
    }
}