using UnityEngine;
using System.Collections;
using PlayerClickers;

public class FireClicker2 : PlayerClicker, IClickerTimer
{
    public override string ClickerName => "Fire Clicker";
    public override string ClickerType => "Fire";
    public override string Description => "Do burn damage over time per click that increases every 15 fire levels. Burn time refreshes on click.";

    private BigNumber BaseDamage;

    public float Timer => 3.0f;
    public float Timer2 { get ; set; }
    public bool TimerInEffect { get ; set ; }

    public override BigNumber Click(int level)
    {
        BaseDamage = new BigNumber(200) * (1 + level / 15);
        Activated = true;
        if (!TimerInEffect)
            Player.Instance.StartCoroutine(ClickTimer());
        else
            Timer2 = 0.0f;

        return BigNumber.Zero;
    }

    public IEnumerator ClickTimer()
    {
        TimerInEffect = true;
        Timer2 = 0.0f;
        while(Timer2 < Timer)
        {
            Debug.Log("Damage!");
            Timer2 += Time.deltaTime;
            Stage.Instance.CurrentEnemy.TakeDamage(BaseDamage * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        TimerInEffect = false;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, Timer);
    }
}
