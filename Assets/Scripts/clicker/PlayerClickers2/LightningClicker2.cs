using UnityEngine;
using PlayerClickers;

public class LightningClicker2 : PlayerClicker2
{
    public override string ClickerName => "Lightning Clicker";

    public override string ClickerType => "Lightning";

    public override string Description => "Chance to deal massive damage that increases every 15 lightning levels.";

    public override BigNumber Click(int level)
    {
        if (Random.value > 0.8f)
        {
            Activated = true;
            return (Player.Instance.TotalClickDamage * (.50f + .25f * (level / 10))).Rounded;
        }
        else
            return BigNumber.Zero;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 0.5f);
    }
}