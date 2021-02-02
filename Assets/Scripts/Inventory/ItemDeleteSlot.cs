using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleteSlot : InventorySlot
{
    
    private void Start()
    {
        _tooltipTrigger.EnableToolTip(true);
        _tooltipTrigger.SetToolTipText("", "Drag equipment here to delete!");
    }
}
