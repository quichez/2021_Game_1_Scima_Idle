using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.scid";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = gameData;
        formatter.Serialize(stream, data);

        stream.Close();

        Application.Quit();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.scid";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}

[System.Serializable]
public class GameData
{
    public double GoldMant;
    public double GoldExp;
    public double ManaMant;
    public double ManaExp;

    public int[] IdlerLevels;
    public bool[][] UpgradesUnlocked;
    public EquipmentData[] InventoryData;
    public EquipmentData[] EquipmentData;

    public GameData()
    {

    }

    public GameData(BigNumber gold, BigNumber mana, List<Equipment> invItemData, List<Equipment> equipData, params IdlerObject[] idlerObjects)
    {
        GoldMant = gold.Mantissa;
        GoldExp = gold.Exponent;
        ManaMant = mana.Mantissa;
        ManaExp = mana.Exponent;
        IdlerLevels = new int[idlerObjects.Length];
        UpgradesUnlocked = new bool[idlerObjects.Length][];
        for (int i = 0; i < idlerObjects.Length; i++)
        {
            
            IdlerLevels[i] = idlerObjects[i].Level;
            UpgradesUnlocked[i] = new bool[idlerObjects[i].UpgradesUnlocked.Length];
            for (int j = 0; j < UpgradesUnlocked[i].Length; j++)
            {
                UpgradesUnlocked[i][j] = idlerObjects[i].UpgradesUnlocked[j];
            }
        }
        Debug.Log(invItemData.Count);
        InventoryData = new EquipmentData[invItemData.Count];
        EquipmentData = new EquipmentData[equipData.Count];

        foreach (Equipment item in invItemData)
        {
            InventoryData[invItemData.IndexOf(item)] = new EquipmentData(item);
        }
        foreach (Equipment item in equipData)
        {
            EquipmentData[equipData.IndexOf(item)] = new EquipmentData(item);
        }

    }
}

[System.Serializable]
public class EquipmentData
{
    public int ID;
    public string Title;
    public string Description;
    public string Slug;
    public int Grade;
    public int Type;
    public float[][] EquipmentStats;

    public EquipmentData(Equipment equipment)
    {
        if (equipment.ID == -1)
            ID = equipment.ID;
        else
        {
            ID = equipment.ID;
            Title = equipment.Title;
            Description = equipment.Description;
            Slug = equipment.Slug;
            Grade = (int)equipment.Grade;
            Type = (int)equipment.Type;

            EquipmentStats = new float[equipment.Stats.Count][];
            Debug.Log(EquipmentStats.Length);
            for (int i = 0; i < EquipmentStats.Length; i++)
            {
                Debug.Log(i);
                EquipmentStats[i] = new float[5];
                EquipmentStats[i][0] = (int)equipment.Stats[i].Idler;
                EquipmentStats[i][1] = (int)equipment.Stats[i].Stat;
                EquipmentStats[i][2] = equipment.Stats[i].Amount;
                EquipmentStats[i][3] = equipment.Stats[i].Min;
                EquipmentStats[i][4] = equipment.Stats[i].Max;
            }       
        }
    }

    public Equipment LoadEquipment()
    {
        try
        {
            if (ID == -1)
                return new Equipment();
            else
                return new Equipment(ID,Title,Description,Slug,Grade,Type,EquipmentStats);
        }
        catch (System.Exception E)
        {
            Debug.Log("Fuck!");
            throw E;
        }
    }
}
