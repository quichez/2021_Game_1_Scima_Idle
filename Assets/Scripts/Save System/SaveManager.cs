using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public GameData Cache { get; private set; }
    private List<IdlerObject> IdlerObjects = new List<IdlerObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }
    
    public static void SaveGame()
    {
        IdlerObjectComparer idobComp = new IdlerObjectComparer();       
        Instance.IdlerObjects.Sort(idobComp);
        IdlerObject[] idobs = new IdlerObject[Instance.IdlerObjects.Count];
        for (int i = 0; i < idobs.Length; i++)
        {
            idobs[i] = Instance.IdlerObjects[i];
        }

        GameData data = new GameData(
            Player.Instance.Gold,
            Player.Instance.Mana,
            Player.Instance.Inventory.Items,
            Player.Instance.Inventory.EquippedItems,
            idobs
            );

        SaveSystem.SaveGame(data);
        
    }   

    public static void LoadGame()
    {
        Instance.Cache = SaveSystem.LoadGame();
    }

    public static void TrackIdlerObject(IdlerObject idob) => Instance?.IdlerObjects.Add(idob);

    public static void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }

}

public class IdlerObjectComparer : IComparer<IdlerObject>
{
    public int Compare(IdlerObject x, IdlerObject y)
    {
        if (x.Index > y.Index)
            return 1;
        else if (y.Index > x.Index)
            return -1;
        else
            return 0;
    }
}