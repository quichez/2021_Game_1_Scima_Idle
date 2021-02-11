using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlerGUIPanel : MonoBehaviour
{
    public PlayerIdlers playerIdlers;
    public IdlerGUI idlerGUIObject;
    public Transform transform2;

    void Start()
    {
        CreateIdlerGUIElements();
    }   

    public void CreateIdlerGUIElements()
    {
        foreach (Idler idler in playerIdlers.IdlerList)
        {
            IdlerGUI clone = Instantiate(idlerGUIObject,transform2) as IdlerGUI;
            clone.SetIdler(idler);
            clone.SetIdlerUpgrades();
            clone.UpdateTextFields();
        }
    }
}
