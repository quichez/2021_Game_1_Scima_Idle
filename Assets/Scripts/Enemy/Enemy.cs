using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float EnemyHealth = 10.0f;
    public float EnemyGold = 5.0f;
    public float EnemyMana = 4.0f;

    public BigNumber Health { get; private set; }
    public BigNumber CurrentHealth { get; private set; }
    public float HealthPercentage => GetPercentageHealth();

    public BigNumber GoldOnKill { get; private set; }
    public BigNumber ManaOnKill { get; private set; }

    private Animator _animator;
    private Rigidbody _rigidbody => GetComponent<Rigidbody>();

    public delegate void OnEnemyUpdated();
    public OnEnemyUpdated OnEnemyHealthUpdated;

    public bool IsDying { get; private set; } = false;

    private LootTable _lootTable => GetComponent<LootTable>();

    public void SetEnemyStats(Zone zone)
    {
        Health = new BigNumber(EnemyHealth * Mathf.Pow(1.1f, zone.Area-1) * Mathf.Pow(1.05f, zone.Enemy-1)).Rounded;
        CurrentHealth = Health;
        GoldOnKill = new BigNumber(EnemyGold * Mathf.Pow(1.05f, zone.Area-1) * Mathf.Pow(1.1f, zone.Enemy-1)).Rounded;
        ManaOnKill = new BigNumber(EnemyMana * Mathf.Pow(1.05f, zone.Area-1) * Mathf.Pow(1.05f, zone.Enemy-1)).Rounded;
    }
    
    public float GetPercentageHealth()
    {
        float pct;
        if (CurrentHealth == BigNumber.Zero)
        {
            pct = 0.0f;
        }
        else
            pct = BigNumber.ReturnAsFloat(CurrentHealth / Health);
        return pct;
    }

    public void TakeDamage(BigNumber amount)
    {
        CurrentHealth = BigNumber.Max(BigNumber.Zero, CurrentHealth - amount);        
        OnEnemyHealthUpdated?.Invoke();
        if ((CurrentHealth <= BigNumber.Zero || BigNumber.ReturnAsFloat(CurrentHealth) <= 0.0f) && IsDying == false)
        {            
            Death();            
        }

    }


    public void Death()
    {
        IsDying = true;
        Debug.Log(CurrentHealth.ToString());
        _lootTable.DropItem();
        Destroy(gameObject, 0.25f);
        OnEnemyHealthUpdated = null;
    }


    public void OnDestroy()
    {
        Player.Instance?.ChangeGold(GoldOnKill);
        Player.Instance?.ChangeMana(ManaOnKill);
        Stage.Instance?.SpawnEnemy();
    }
}
