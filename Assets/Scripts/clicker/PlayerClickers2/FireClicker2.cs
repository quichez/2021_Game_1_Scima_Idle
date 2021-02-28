using UnityEngine;
using System.Collections;
using PlayerClickers;

public class FireClicker2 : PlayerClicker2, IClickerTimer
{
    public override string ClickerName => "Fire Clicker";

    public override string ClickerType => "Fire";

    public override string Description => "Do stacking burn damage over time per click that increases every 15 fire levels.";

    public float Timer => 3.0f;
    public float Timer2 { get ; set; }
    public bool TimerInEffect { get ; set ; }

    public override BigNumber Click(int level)
    {
        return BigNumber.Zero;
    }

    public IEnumerator ClickTimer()
    {
        throw new System.NotImplementedException();
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 3.0f);
    }
}
