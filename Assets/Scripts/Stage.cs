using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Zone
{
    public int Area { get; private set; }
    public int Enemy { get; private set; }

    public Zone(int area, int enemy)
    {
        Area = area;
        Enemy = enemy;
    }

    /// <summary>
    /// Advance the zone one step.
    /// </summary>
    public static Zone Increment(Zone zone)
    {
        if ((zone.Enemy + 1) > 25)
        {
            zone.Enemy = 1;
            zone.Area += 1;
        }
        else
            zone.Enemy += 1;
        return new Zone(zone.Area, zone.Enemy);
    }
}

public class Stage : MonoBehaviour
{
    public static Stage Instance;
    public Zone Zone { get; private set; } = new Zone(1, 1);
    public EnemyTable[] Enemies;
    public Enemy CurrentEnemy {get; private set; }
    public ZoneNumber _zoneNumber; // The UI Element stupid

    public Transform spawnLocation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        if (SaveManager.Instance?.Cache != null)
            Zone = new Zone(SaveManager.Instance.Cache.zoneNumber[0], SaveManager.Instance.Cache.zoneNumber[1]);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {      
        _zoneNumber.UpdateZoneNumber(Zone);
        CurrentEnemy = Instantiate(Enemies[Zone.Area % 3].EnemyList[Random.Range(0, Enemies[Zone.Area % 3].EnemyList.Count)],spawnLocation);
        CurrentEnemy.SetEnemyStats(Zone);
        HealthBar.Instance.Subscribe(CurrentEnemy);
        HealthBar.Instance.UpdateHealthBar();
        Zone = Zone.Increment(Zone);
    }   
}
