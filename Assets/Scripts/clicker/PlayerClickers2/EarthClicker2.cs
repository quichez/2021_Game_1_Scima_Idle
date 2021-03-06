using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerClickers;

public class EarthClicker2 : PlayerClicker, IClickerTimer
{
    public override string ClickerName => "Earth Clicker";

    public override string ClickerType => "Earth";

    public override string Description => "Deals extra damage on consecutive clicks that increases every 10 earth levels.";

    public float Timer => 1.0f;
    public float Timer2 { get; set; }
    public bool TimerInEffect { get; set; }
    private int _combo = 0;

    public override BigNumber Click(int level)
    {
        Activated = true;
        if(!TimerInEffect)
            Player.Instance.StartCoroutine(ClickTimer());
        else
        {
            Timer2 = 0.0f;
            _combo += 1;
        }
        return (new BigNumber(10) + new BigNumber(5) * (level/10)) * _combo;
    }

    public override void DelayedDestroy(GameObject effect)
    {
        Object.Destroy(effect, 0.5f);
    }

    public IEnumerator ClickTimer()
    {
        TimerInEffect = true;
        Timer2 = 0.0f;
        while(Timer2 < Timer)
        {
            Timer2 += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        TimerInEffect = false;
        _combo = 0;
    }
}
