using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot
{
    public EquipmentType SlotType;

    public void SetEquipmentItem(Equipment item)
    {
        if (item.ID != -1)
        {
            RemoveItem();
            if (item.Type == SlotType)
                SetItem(item);
        }
        else
            RemoveItem();
    }

    public void RemoveEquipment()
    {
        SetEquipmentItem(new Equipment());
    }

    public new void OnPointerDown(PointerEventData evenData)
    {
        Debug.Log("nope");
    }
    public new void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("please equip");
    }
    public new void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped on equipment" + name);
    }
}
