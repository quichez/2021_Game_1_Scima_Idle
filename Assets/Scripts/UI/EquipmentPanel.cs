using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    private void UpdateEquipment()
    {
        //Player.Instance?.OnInventoryUpdateCallback?.Invoke();
    }

    private void OnEnable()
    {
        UpdateEquipment();
    }
}
