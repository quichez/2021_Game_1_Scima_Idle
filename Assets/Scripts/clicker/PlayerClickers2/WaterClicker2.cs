using UnityEngine;
using PlayerClickers;

public class WaterClicker2 : PlayerClicker
{
    public override string ClickerName => "Water Clicker";

    public override string ClickerType => "Water";

    public override string Description => "Gain extra mana on click that increases every 15 water levels.";

    public override BigNumber Click(int level)
    {
        Player.Instance.ChangeMana(new BigNumber(10) * (1 + level / 15));
        return BigNumber.Zero;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 0.5f);
    }
}