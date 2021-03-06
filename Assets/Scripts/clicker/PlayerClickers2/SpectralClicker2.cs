using UnityEngine;
using System.Collections;
using PlayerClickers;

public class SpectralClicker2 : PlayerClicker
{
    public override string ClickerName => "Spectral Clicker";

    public override string ClickerType => "Spectral";

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