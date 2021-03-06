using UnityEngine;
using System.Collections;
using PlayerClickers;

public class FairyClicker2 : PlayerClicker
{
    public override string ClickerName => "Fairy Clicker";

    public override string ClickerType => "Fairy";

    public override string Description => "Figure this out later!";

    public override BigNumber Click(int level)
    {
        throw new System.NotImplementedException();
    }

    public override void DelayedDestroy(GameObject effect)
    {
        throw new System.NotImplementedException();
    }
}
