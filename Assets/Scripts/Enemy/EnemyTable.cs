using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Table", menuName = "Enemy/Enemy Table")]
public class EnemyTable : ScriptableObject
{
    public List<Enemy> EnemyList;
}
