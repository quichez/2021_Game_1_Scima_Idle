using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdlerLevelUpButton : MonoBehaviour
{
    private Idler _idler => transform.GetComponentInParent<Idler>();
    private Button _button => GetComponent<Button>();
    private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    private TooltipTrigger _tooltip => GetComponent<TooltipTrigger>();

    private void Start()
    {
        _tooltip.SetToolTipText("Level Up " + _idler.IdlerObject.name + " by One!",
            "+ " + _idler.IdlerObject.BaseDamagePerLevel.ToString() + " Base DPS\n" +
            "+ " + _idler.IdlerObject.BaseManaPerLevel.ToString() + " Base MPS");
    }

    private void Update()
    {
        if ((Player.Instance.Gold - _idler.Cost) >= BigNumber.Zero)
            _button.interactable = true;
        else
            _button.interactable = false;
    }

    public void UpdateButtonText()
    {
        _text.text = _idler.Cost.Rounded.ToString() + "\n Level Up";
    }

    public void LevelUp()
    {
        Player.Instance.ChangeGold(-_idler.Cost.Rounded);
        _idler.IdlerObject.Level += 1;
        _idler.ModifyTotalDamage();
        _idler.ModifyTotalCost();
        UpdateButtonText();
    }
}
