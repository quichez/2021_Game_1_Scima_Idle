using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IdlerUpgradeType { Cost,Damage,Mana,Clicker}

[RequireComponent(typeof(TooltipTrigger))]
public abstract class IdlerUpgrade : MonoBehaviour
{
    protected Image _image => GetComponent<Image>();

    [Header("Upgrade Cost")]
    [Range(0.000f,9.999f)] public double CostMantissa;
    public double CostExponent;
    public BigNumber UpgradeCost => new BigNumber(CostMantissa, CostExponent);

    protected Button _button => GetComponent<Button>();
    protected Idler Idler => transform.parent.parent.GetComponent<Idler>();
    public bool Unlocked { get; private set; } = false;

    protected TooltipTrigger _tooltipTrigger => GetComponent<TooltipTrigger>();

    private void Awake()
    {
        _button.onClick.AddListener(delegate { UnlockUpgrade(); });
    }

    private void Update()
    {
        _button.interactable = (Player.Instance?.Gold - UpgradeCost >= BigNumber.Zero) && Unlocked == false;
    }

    public virtual void UnlockUpgrade()
    {
        Unlocked = true;
        Idler.IdlerObject.UpgradesUnlocked[transform.GetSiblingIndex()] = true;
        _image.color = Color.blue + Color.gray;
        Player.Instance?.ChangeGold(-UpgradeCost);
    }

}
