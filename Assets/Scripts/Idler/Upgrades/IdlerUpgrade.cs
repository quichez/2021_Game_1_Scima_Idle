using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IdlerUpgradeType { Cost, Damage, Mana, Clicker }

[RequireComponent(typeof(TooltipTrigger))]
public abstract class IdlerUpgrade : MonoBehaviour
{
    protected Image _image =>   GetComponent<Image>();
    protected Button _button => GetComponent<Button>();
    protected TooltipTrigger _tooltipTrigger => GetComponent<TooltipTrigger>();
    protected Idler _idler;

    protected BigNumber UpgradeCost;
    protected float amount;
    protected bool Unlocked;

    private void Awake()
    {
        _button.onClick.AddListener(delegate { UnlockUpgrade(); });
    }

    private void Update()
    {
        _button.interactable = (Player.Instance.Gold - UpgradeCost >= BigNumber.Zero) && Unlocked == false;
    }

    public void SetUpgrade(Idler idler, int i)
    {
        _idler = idler;

        IdlerObjectUpgrade idobUpgrade = idler.IdlerObject.idlerUpgrades[i];
        UpgradeCost = idobUpgrade.UpgradeCost;
        amount = idobUpgrade.amount;
        Unlocked = idobUpgrade.unlocked;
        if(Unlocked)
            _image.color = Color.blue / 0.75f + Color.gray / 1.5f;
    }


    public virtual void UnlockUpgrade()
    {
        Unlocked = true;
        Player.Instance.ChangeGold(-UpgradeCost);
        _idler.IdlerObject.idlerUpgrades[transform.GetSiblingIndex()].unlocked = true;
        _image.color = Color.blue/0.75f + Color.gray/1.5f;
        _idler.OnLevelUpCallback?.Invoke();
    }
}
