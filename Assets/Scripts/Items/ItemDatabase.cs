using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    private JsonData _equipmentDatabase;

    public static List<Equipment> EquipmentDatabase { get; set; } = new List<Equipment>();

    private void Awake()
    {
        BuildDatabase();
    }

    public void BuildDatabase()
    {
        _equipmentDatabase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Equipment.json"));
        foreach (JsonData item in _equipmentDatabase)
        {
            EquipmentDatabase.Add(
                new Equipment((int)item["id"],
                item["title"].ToString(),
                item["desc"].ToString(),
                item["slug"].ToString(),
                (EquipmentType)Enum.Parse(typeof(EquipmentType), item["equip_type"].ToString(),true),
                item["stats"]));
                
        }
    }

    public static Equipment GetItemByID(int id)
    {
        return new Equipment();
    }

    public static Equipment GetEquipmentByID(int id)
    {
        return EquipmentDatabase.Find(obj => obj.ID == id);
    }
}
