using UnityEngine;
using System.Collections;
using PlayerClickers;

public class LightClicker2 : PlayerClicker2
{
    public override string ClickerName => "Light Clicker";

    public override string ClickerType => "Light";

    public override string Description => "Does extra missing health damage that increases every 20 light levels.";

    public override BigNumber Click(int level)
    {
        throw new System.NotImplementedException();
    }

    public override void DelayedDestroy(GameObject effect)
    {
        throw new System.NotImplementedException();
    }
}
