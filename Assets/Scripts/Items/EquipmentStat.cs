using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using LitJson;

public class EquipmentStat
{
    public IdlerName Idler { get; private set; }
    public IdlerStat Stat { get; private set; }
    public float Min { get; private set; }
    public float Max { get; private set; }
    public float Amount { get; private set; }

    public EquipmentStat() //Default Constructor that isn't all null (Debug only)
    {
        Idler = IdlerName.Air;
        Stat = IdlerStat.Damage;
        Amount = 1.0f;
    }

    public EquipmentStat(IdlerName idler, IdlerStat stat)
    {
        Idler = idler;
        Stat = stat;
    }

    public EquipmentStat(IdlerName idler, IdlerStat stat, JsonData amount)
    {
        Idler = idler;
        Stat = stat;
        Min = Convert.ToSingle((double)amount["min"]);
        Max = Convert.ToSingle((double)amount["max"]);
        Amount = (float) Math.Round(UnityEngine.Random.Range(Min, Max), 2);
    }

    public EquipmentStat(IdlerName idler, IdlerStat stat, JsonData amount, float gradeModifier)
    {
        Idler = idler;
        Stat = stat;
        Min = Convert.ToSingle((double)amount["min"]);
        Max = Convert.ToSingle((double)amount["max"]);
        Amount = (float) Math.Round(gradeModifier * UnityEngine.Random.Range(Min, Max), 2);
    }

    public EquipmentStat(float[] stat)
    {        
        Idler = (IdlerName)stat[0];
        Stat = (IdlerStat)stat[1];
        Min = stat[2];
        Max = stat[3];
        Amount = (float) Math.Round(stat[4], 2);
    }
    public override string ToString()
    {
        if (Stat == IdlerStat.Cost || Stat == IdlerStat.Mana)
            return "Reduces " + Idler.ToString() + " " + Stat.ToString() + " by " + Math.Round((1 / Amount),2).ToString() + ".\n";
        else
            return "Increases " + Idler.ToString() + " Damage by " + Amount.ToString() + ".\n";
    }
}
