using System.Collections.Generic;
using UnityEngine;
using WeightedChance;

public class LootTable : MonoBehaviour
{
    [Tooltip("No drop weight = 100")]
    public int dropWeight = 10;
    public List<int> EquipmentDrops;
    public List<int> GradeWeights;
    private int _dropChance; 

    public void DropItem()
    {
        WeightedRoll chanceToDrop = new WeightedRoll(
            new WeightedParam(() => _dropChance = 0, 100),
            new WeightedParam(() => _dropChance = 1, dropWeight));
        chanceToDrop.Execute();

        Debug.Log(_dropChance);
        if(_dropChance == 1)
        {
            int equipRoll = Random.Range(0, EquipmentDrops.Capacity);
            IdlerName idler = (IdlerName)Random.Range(0, 7);

            Equipment drop = new Equipment(ItemDatabase.GetEquipmentByID(EquipmentDrops[equipRoll]));

            WeightedRoll gradeRoll = new WeightedRoll(
                new WeightedParam(() => drop.SelectGrade(GradeType.Common), GradeWeights[0]),
                new WeightedParam(() => drop.SelectGrade(GradeType.Uncommon), GradeWeights[1]),
                new WeightedParam(() => drop.SelectGrade(GradeType.Rare), GradeWeights[2]),
                new WeightedParam(() => drop.SelectGrade(GradeType.Epic), GradeWeights[3]),
                new WeightedParam(() => drop.SelectGrade(GradeType.Legendary), GradeWeights[4]));

            gradeRoll.Execute();
            drop.SetStats(idler);

            Player.Instance.AddItem(drop);
        }
    } 
}
