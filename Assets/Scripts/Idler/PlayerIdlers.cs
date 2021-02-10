using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlers : MonoBehaviour
{
    public IdlerObject2[] IdlerObjects;
    public List<Idler2> IdlerList { get; private set; }
    public Idler2[] Idlers { get; private set; } 

    public BigNumber TotalIdlerDamage;
    public BigNumber TotalManaCost;

    private List<float> _damageModifiers = new List<float>(20);
    private List<float> _manaModifiers = new List<float>(20);
    private List<float> _costModifiers = new List<float>(20);

    private void Awake()
    {
        //Create Idler2 for every IdOb2
        Idlers = new Idler2[IdlerObjects.Length];
        IdlerList = new List<Idler2>(IdlerObjects.Length);
        for (int i = 0; i < IdlerObjects.Length; i++)
        {
            //Set Idler2 values to IdOb2 values
            Idlers[i] = new Idler2(IdlerObjects[i]);
            IdlerList.Add(new Idler2(IdlerObjects[i]));
        }
    }

    void Start()
    {
        



        //Update GUI
        //Subscribe methods to OnEquipCallback
    }    

    public void GetEquipmentModifiers()
    {
        _damageModifiers.Clear();
        _manaModifiers.Clear();
        _costModifiers.Clear();

        foreach (Equipment equip in Player.Instance.Inventory.EquippedItems)
        {
            if(equip.ID != -1)
            {
                foreach (EquipmentStat stat in equip.Stats)
                {
                    Idler2 idler = IdlerList.Find(x => x.IdlerObject.IdlerName == stat.Idler);                    
                }
            }
        }
        
    }

    public void UpdateTotalIdlerDamage()
    {
        BigNumber temp = BigNumber.Zero;
        foreach (Idler2 idler in Idlers)
        {
            temp += idler.Damage;
        }
        TotalIdlerDamage = temp;
    }

    public void UpdateTotalIdlerManaCost()
    {
        BigNumber temp = BigNumber.Zero;
        foreach (Idler2 idler in Idlers)
        { 
            temp += idler.Mana;
        }
        TotalManaCost = temp;
    }


    //Methods
    //Probably all idlers
    //Call all PlayerClicks?
}
