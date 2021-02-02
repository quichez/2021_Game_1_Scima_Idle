using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentTooltipTrigger : TooltipTrigger
{
    private InventorySlot slot => GetComponent<InventorySlot>();

    public new void OnPointerEnter(PointerEventData eventData)
    {

    }
}
