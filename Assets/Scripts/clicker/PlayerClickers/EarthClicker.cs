using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthClicker : PlayerClicker, IPlayerClick
{
    public float Timer = 0.5f;
    public int IncreaseAmount;

    private float _timer = 0.0f;
    private int _combo;

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= Timer)
        {
            _combo = 0;
            _timer = 0.0f;
        }
    }

    public void PlayerClick()
    {
        if (Unlocked)
        {
            Stage.Instance.CurrentEnemy.TakeDamage(BaseDamage + IncreaseAmount * (1 + _idler.IdlerObject.Level/10)* _combo);
            _combo += 1;
            _timer = 0.0f;
        }            
    }

    public override string ToString()
    {
        return "Earth Clicker";
    }

    public override string Description()
    {
        return "Consecutive clicks deal even more damage, increasing every 10 Earth levels!";
    }
}
