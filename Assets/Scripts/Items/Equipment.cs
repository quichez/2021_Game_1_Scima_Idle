using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using LitJson;

public enum EquipmentType
{
    [EnumMember(Value = "hat")] Hat,
    [EnumMember(Value = "chest")] Chest,
    [EnumMember(Value = "gloves")] Gloves,
    [EnumMember(Value = "boots")] Boots,
    [EnumMember(Value = "weapon")] Weapon,
    [EnumMember(Value = "accesory")] Accesory
}


public class Equipment : Item<Equipment>
{
    public EquipmentType Type { get; private set; }
    public List<EquipmentStat> Stats { get; private set; }
    public JsonData StatRanges { get; private set; }

    public Equipment() : base()
    {

    }

    public Equipment(Equipment frame)
    {
        ID = frame.ID;
        Title = frame.Title;
        Description = frame.Description;
        Slug = frame.Slug;
        Type = frame.Type;
        StatRanges = frame.StatRanges;
        Stats = new List<EquipmentStat>();

        Icon = Resources.Load<Sprite>(Slug);
    }

    public override Equipment Test(Equipment frame)
    {
        return new Equipment(frame);
    }

    public Equipment(int id, string title, string desc, string slug, EquipmentType type, JsonData statRanges) : base(id, title, desc, slug)
    {
        Stats = new List<EquipmentStat>();
        StatRanges = statRanges;
        Type = type;
    }

    public Equipment(int id, string title,string desc, string slug, int grade, int type, float[][] stats)
    {
        ID = id;
        Title = title;
        Description = desc;
        Slug = slug;
        Grade = (GradeType)grade;
        Type = (EquipmentType)type;
        Stats = new List<EquipmentStat>();
        LoadStats(stats);
        Icon = Resources.Load<Sprite>(Slug);
        SelectGrade(Grade);
    }

    public void SetStats(IdlerName idler)
    {
        int roll;
        List<IdlerStat> idlerStats = new List<IdlerStat>(3) { IdlerStat.Damage, IdlerStat.Cost, IdlerStat.Mana };
        List<JsonData> stats = new List<JsonData>(3) { StatRanges["damage"], StatRanges["cost"], StatRanges["mana"] };

        switch (Grade)
        {
            
            case GradeType.Common: // 1 stat
                roll = UnityEngine.Random.Range(0, 3);
                Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll]));
                break;

            case GradeType.Uncommon: // 2 stat
                for (int i = 0; i < 2; i++)
                {
                    roll = UnityEngine.Random.Range(0, 3-i);
                    Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll]));
                    idlerStats.Remove(idlerStats[roll]);
                    stats.Remove(stats[roll]);
                }
                break;

            case GradeType.Rare: // 2 stat +
                for (int i = 0; i < 2; i++)
                {
                    roll = UnityEngine.Random.Range(0, 3 - i);
                    Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll], 1.2f));
                    idlerStats.Remove(idlerStats[roll]);
                    stats.Remove(stats[roll]);
                }
                break;

            case GradeType.Epic: // 3 stat
                for (int i = 0; i < 3; i++)
                {
                    roll = UnityEngine.Random.Range(0, 3 - i);
                    Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll]));
                    idlerStats.Remove(idlerStats[roll]);
                    stats.Remove(stats[roll]);
                }
                break;

            case GradeType.Legendary: // 3 stat +
                for (int i = 0; i < 3; i++)
                {
                    roll = UnityEngine.Random.Range(0, 3 - i);
                    Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll], 1.2f));
                    idlerStats.Remove(idlerStats[roll]);
                    stats.Remove(stats[roll]);
                }
                break;

            case GradeType.Unique: // 3 stat ++
                for (int i = 0; i < 3; i++)
                {
                    roll = UnityEngine.Random.Range(0, 3 - i);
                    Stats.Add(new EquipmentStat(idler, idlerStats[roll], stats[roll], 1.5f));
                    idlerStats.Remove(idlerStats[roll]);
                    stats.Remove(stats[roll]);
                }
                break;

            default:
                Stats.Add(new EquipmentStat());
                break;
        }
    }

    public void LoadStats(float[][] stats)
    {
        foreach (float[] item in stats)
        {
            Stats.Add(new EquipmentStat(item));
        }
    }

    public string StatsToString()
    {
        string result = "";
        
        foreach (EquipmentStat stat in Stats)
        {
            result += stat.ToString();
        }
        return result;
    }

}
