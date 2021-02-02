using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public int InventorySpace = 24;
    public int EquipmentSpace = 6;

    public BigNumber Gold { get; private set; } = BigNumber.Zero;
    public BigNumber Mana { get; private set; } = BigNumber.Zero;

    public Inventory Inventory { get; private set; }

    public GameObject IdlerList;
    public Idler[] Idlers => IdlerList.GetComponentsInChildren<Idler>();
    public IPlayerClick[] PlayerClickers => GetComponents<IPlayerClick>();

    public BigNumber Damage => TotalIdlerDamage() * Time.deltaTime;
    public BigNumber ManaCost => TotalIdlerManaCost() * Time.deltaTime;


    public delegate void OnPlayerUpdate();
    public OnPlayerUpdate OnInventoryUpdateCallback;
    public OnPlayerUpdate OnGoldUpdateCallback;
    public OnPlayerUpdate OnManaUpdateCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        Inventory = new Inventory(InventorySpace, EquipmentSpace);       
    }

    private void Start()
    {
        
        GameData playerCache = SaveManager.Instance?.Cache;
        if (playerCache != null)
        {
            if (!double.IsNaN(playerCache.GoldMant) && !double.IsNaN(playerCache.GoldExp))
                Gold = new BigNumber(playerCache.GoldMant, playerCache.GoldExp).Rounded;                            
            if (!double.IsNaN(playerCache.ManaMant) && !double.IsNaN(playerCache.ManaExp))
                Mana = new BigNumber(playerCache.ManaMant, playerCache.ManaExp).Rounded;
            Debug.Log(playerCache.InventoryData.Length);
            Inventory.Items.Clear();
            Inventory.EquippedItems.Clear();
            foreach (EquipmentData item in playerCache.InventoryData)
            {
                Equipment eqp = item.LoadEquipment();
                try
                {
                    Inventory.Items.Add(item.LoadEquipment());
                }
                catch (Exception E)
                {
                    Debug.Log("fucked " + eqp);
                    throw E;
                }
            }
            foreach (EquipmentData item in playerCache.EquipmentData)
            {
                Inventory.EquippedItems.Add(item.LoadEquipment());
            }
        }
        OnGoldUpdateCallback?.Invoke();
        OnManaUpdateCallback?.Invoke();
    }

    private void Update()
    {
        if (Mana > BigNumber.Zero && !Stage.Instance.CurrentEnemy.IsDying)
        {
            Mana = BigNumber.Max(BigNumber.Zero, Mana - ManaCost);
            Stage.Instance.CurrentEnemy?.TakeDamage(Damage);
        }        
        OnManaUpdateCallback?.Invoke();
    }

    public BigNumber TotalIdlerDamage()
    {
        BigNumber dmg = new BigNumber();
        foreach (Idler idler in Idlers)
        {
            dmg += idler.Damage;
        }        
        return dmg;
    }

    public BigNumber TotalIdlerManaCost()
    {
        BigNumber mana = new BigNumber();
        foreach (Idler idler in Idlers)
        {
            mana += idler.Mana;
        }
        return mana;
    }

    public void AddItem(Equipment item)
    {
        for (int i = 0; i < InventorySpace; i++)
        {
            if (Inventory.Items[i].ID == -1)
            {
                Inventory.Items[i] = item;
                break;
            }
            if(i==InventorySpace-1)
                Debug.Log("Inventory is full");
        }
        OnInventoryUpdateCallback?.Invoke();
    }

    public void SwapItems(InventorySlot a, InventorySlot b)
    {
        int one = a.transform.GetSiblingIndex();
        int two = b.transform.GetSiblingIndex();

        Equipment temp = Inventory.Items[one];
        Inventory.Items[one] = Inventory.Items[two];
        Inventory.Items[two] = temp;

        OnInventoryUpdateCallback?.Invoke();
    }

    public void EquipItem(InventorySlot item, EquipmentSlot destination)
    {
        if(destination.SlotType == ((Equipment)item.Item).Type || item.Item.ID == -1)
        {
            int one = item.transform.GetSiblingIndex();
            int two = destination.transform.GetSiblingIndex();

            Equipment temp = Inventory.Items[one];
            Inventory.Items[one] = Inventory.EquippedItems[two];
            Inventory.EquippedItems[two] = (Equipment)temp;

            OnInventoryUpdateCallback?.Invoke();
        }
    }

    public void SwapEquip(EquipmentSlot equipment, InventorySlot inventorySlot)
    {
        if( inventorySlot.Item.ID == -1 || equipment.SlotType == ((Equipment)inventorySlot.Item).Type)
        {
            int one = equipment.transform.GetSiblingIndex();
            int two = inventorySlot.transform.GetSiblingIndex();

            Equipment temp = Inventory.EquippedItems[one];
            

            Inventory.EquippedItems[one] = (Equipment) Inventory.Items[two];               
            Inventory.Items[two] = temp;
        }
        OnInventoryUpdateCallback?.Invoke();
    }

    public void RemoveItem(InventorySlot removal)
    {
        int i = Inventory.Items.FindIndex(x => x == removal.Item);
        Inventory.Items[i] = new Equipment();
        OnInventoryUpdateCallback?.Invoke();
    }
    
    public void DeleteEquippedItem(EquipmentSlot removal)
    {
        int i = Inventory.EquippedItems.FindIndex(x => x == removal.Item);
        Inventory.EquippedItems[i] = new Equipment();
        OnInventoryUpdateCallback?.Invoke();
    }

    public void ChangeGold(BigNumber amount)
    {
        Gold += amount.Rounded;
        OnGoldUpdateCallback?.Invoke();
    }

    public void ChangeMana(BigNumber amount)
    {
        Mana += amount.Rounded;
        OnManaUpdateCallback?.Invoke();
    }

    public void PlayerClick()
    {
        Stage.Instance.CurrentEnemy.TakeDamage(new BigNumber(10));
        foreach (IPlayerClick click in PlayerClickers)
        {
            click.PlayerClick();
        }
    }
}

public class Inventory
{
    public List<Equipment> Items { get; private set; }
    public List<Equipment> EquippedItems { get; private set; }

    public Inventory(int invCapacity, int EquipCapacity)
    {
        Items = new List<Equipment>(invCapacity);
        EquippedItems = new List<Equipment>(EquipCapacity);
        for (int i = 0; i < invCapacity; i++)
        {
            Items.Add(new Equipment());
            if (i < EquipCapacity)
                EquippedItems.Add(new Equipment()); 
        }
    }       
}
