using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public GameObject EquipmentInventory;
    public GameObject EquippedItems;

    private InventorySlot[] _slots;      
    private EquipmentSlot[] _eSlots;

    private void Start()
    {
        Player.Instance.OnInventoryUpdateCallback += UpdateInventoryPanel;
        _slots = EquipmentInventory.GetComponentsInChildren<InventorySlot>();
        _eSlots = EquippedItems.GetComponentsInChildren<EquipmentSlot>();
    }

    private void UpdateInventoryPanel()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SetItem(Player.Instance.Inventory.Items[i]);
        }
        for (int i = 0; i < _eSlots.Length; i++)
        {
            _eSlots[i].SetEquipmentItem(Player.Instance.Inventory.EquippedItems[i]);
        }
    }
    
    #region OnEnable and OnDisable
    private void OnEnable()
    {
        if(Player.Instance)
            Player.Instance.OnInventoryUpdateCallback += UpdateInventoryPanel;
    }

    private void OnDisable()
    {
        Player.Instance.OnInventoryUpdateCallback -= UpdateInventoryPanel;
    }
    #endregion
}
