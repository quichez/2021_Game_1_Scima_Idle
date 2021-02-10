using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdlerLevelUpButton2 : MonoBehaviour
{
    private Idler2 _idler;
    private Button _button =>           GetComponent<Button>();
    public TextMeshProUGUI _text;// =>    GetComponent<TextMeshProUGUI>();
    private TooltipTrigger _tooltip =>  GetComponent<TooltipTrigger>();

    private void Update()
    {
        _button.interactable = Player.Instance.Gold - _idler.Cost >= BigNumber.Zero;
    }

    public void Subscribe(Idler2 idler)
    {
        //Cache reference to subscribed idler
        _idler = idler;

        //Set up Tooltip
        _tooltip.SetToolTipText("Level Up " + _idler.IdlerObject.name + " by One!",
            "+ " + _idler.IdlerObject.BaseDamagePerLevel.ToString() + " Base DPS\n" +
            "+ " + _idler.IdlerObject.BaseManaPerLevel.ToString() + " Base MPS");
    }

    public void UpdateButtonText()
    {
        _text.text = _idler?.Cost?.Rounded.ToString() + "\n Level Up";
    }
    
    public void LevelUpIdler(int amount)
    {
        _idler.LevelUp();
    }
}
