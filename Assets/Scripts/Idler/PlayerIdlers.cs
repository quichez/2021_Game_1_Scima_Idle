using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlers : MonoBehaviour
{
    public IdlerObject[] IdlerObjects;
    public List<Idler> IdlerList { get; private set; }

    public BigNumber TotalIdlerDamage { get; private set; }
    public BigNumber TotalManaCost { get; private set; }

    public BigNumber DamagePerFrame => TotalIdlerDamage * Time.deltaTime;
    public BigNumber ManaPerFrame => TotalManaCost * Time.deltaTime;

    private List<float> _damageModifiers = new List<float>(20);
    private List<float> _manaModifiers = new List<float>(20);
    private List<float> _costModifiers = new List<float>(20);

    public delegate void OnPlayerIdlerUpdate();
    public OnPlayerIdlerUpdate OnPlayerDamageUpdateCallback;
    public OnPlayerIdlerUpdate OnPlayerManaUpdateCallback;
    public OnPlayerIdlerUpdate OnPlayerClickerUpdateCallback;

    private void Awake()
    {
        //Create Idler2 for every IdOb2
        IdlerList = new List<Idler>(IdlerObjects.Length);
        for (int i = 0; i < IdlerObjects.Length; i++)
        {
            //Set Idler2 values to IdOb2 values
            IdlerList.Add(new Idler(IdlerObjects[i],this));
            if(SaveManager.Instance?.Cache != null)
                IdlerList[i].LoadData(SaveManager.Instance.Cache.UpgradesUnlocked[i], SaveManager.Instance.Cache.IdlerLevels[i]);
        }
    }

    void Start()
    {
        UpdateTotalIdlerDamage();
        UpdateTotalIdlerManaCost();
        Player.Instance.OnEquipCallback += UpdateTotalIdlerDamage;
        Player.Instance.OnEquipCallback += UpdateTotalIdlerManaCost;
    } 

    public void UpdateTotalIdlerDamage()
    {        
        BigNumber temp = BigNumber.Zero;
        foreach (Idler idler in IdlerList)
        {
            temp += idler.Damage;
        }
        TotalIdlerDamage = temp;
        OnPlayerDamageUpdateCallback?.Invoke();
    }

    public void UpdateTotalIdlerManaCost()
    {
        BigNumber temp = BigNumber.Zero;
        foreach (Idler idler in IdlerList)
        { 
            temp += idler.Mana;
        }
        TotalManaCost = temp;
        OnPlayerManaUpdateCallback?.Invoke();
    }

    public void OnPlayerClick()
    {
        if (Stage.Instance.CurrentEnemy.IsDying)
            return;

        BigNumber tempClick = BigNumber.Zero;
        tempClick += Player.Instance.TotalClickDamage;
        foreach (Idler idler in IdlerList)
        {
            for (int i = 0; i < idler.IdlerObject.idlerUpgrades.Length; i++)
            {
                if(idler.IdlerObject.idlerUpgrades[i].type == IdlerUpgradeType.Clicker)
                {                    
                    if (idler.IdlerObject.idlerUpgrades[i].unlocked)
                    {
                        tempClick += idler._playerClicker.Click(idler.IdlerObject.Level);
                        if (idler._playerClicker.Activated)
                        {
                            GameObject xf = EffectSpawner.Instance.InstantiateEffect(idler.IdlerObject.clickerEffect);
                            idler._playerClicker.Deactivate();
                            idler._playerClicker.DelayedDestroy(xf);
                        }
                    }
                }
            }
        }
        Stage.Instance.CurrentEnemy.TakeDamage(tempClick);
        TestDamagePopUpController.Instance.PopUpDamage(tempClick);
    }

    //Methods
    //Probably all idlers
    //Call all PlayerClicks?
}
