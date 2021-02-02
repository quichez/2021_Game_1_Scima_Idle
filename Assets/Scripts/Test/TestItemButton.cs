using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeightedChance;

public class TestItemButton : MonoBehaviour
{
    public List<int> LootTable = new List<int>();
    public void InstantiateAnItem()
    {
        Equipment clone = new Equipment(ItemDatabase.GetEquipmentByID(LootTable[Random.Range(0, LootTable.Capacity)]));
        IdlerName idler = (IdlerName)Random.Range(0, 7);

        WeightedRoll roll = new WeightedRoll(
            new WeightedParam(() => clone.SelectGrade(GradeType.Common), 30),
            new WeightedParam(() => clone.SelectGrade(GradeType.Uncommon), 10),
            new WeightedParam(() => clone.SelectGrade(GradeType.Rare), 5),
            new WeightedParam(() => clone.SelectGrade(GradeType.Epic), 3),
            new WeightedParam(() => clone.SelectGrade(GradeType.Legendary), 1));

        roll.Execute();

        clone.SetStats(idler);

        Player.Instance.AddItem(clone);
    }
}
