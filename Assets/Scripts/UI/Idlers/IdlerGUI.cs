using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdlerGUI : MonoBehaviour
{
    [Header("Idler Information")]
    public Image Icon;
    public TextMeshProUGUI IdlerName;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI ManaText;

    public IdlerLevelUpButton2 Button;
    public IdlerUpgradeGroup2 IdlerUpgradeGroup;

    private Idler2 _idler;

    public void SetIdler(Idler2 idler)
    {
        _idler = idler;
        Icon.sprite = _idler.IdlerObject.Icon;
        Button.Subscribe(idler);
        _idler.OnLevelUpCallback += UpdateTextFields;
    }

    public void SetIdlerUpgrades()
    {
        for (int i = 0; i < IdlerUpgradeGroup.transform.childCount; i++)
        {
            GameObject upgradeButton = IdlerUpgradeGroup.transform.GetChild(i).gameObject;
            switch (_idler.IdlerObject.idlerUpgrades[i].type)
            {
                case IdlerUpgradeType.Cost:
                    upgradeButton.AddComponent<CostUpgrade2>();
                    break;
                case IdlerUpgradeType.Damage:
                    upgradeButton.AddComponent<DamageUpgrade2>();
                    break;
                case IdlerUpgradeType.Mana:
                    upgradeButton.AddComponent<ManaUpgrade2>();
                    break;
                case IdlerUpgradeType.Clicker:
                    upgradeButton.AddComponent<ClickerUnlock2>();
                    upgradeButton.GetComponent<ClickerUnlock2>().SetIdler(_idler);
                    break;
                default:
                    break;
            }
            upgradeButton.GetComponent<IdlerUpgrade2>().SetUpgrade(_idler,i);
            
        }        
    }

    public void UpdateTextFields()
    {
        IdlerName.text = _idler.IdlerObject.IdlerName.ToString();
        LevelText.text = "Level " + _idler.IdlerObject.Level.ToString();
        DamageText.text = _idler.Damage.ToString() + " DPS";
        ManaText.text = _idler.Mana.ToString() + " MPS";
        Button.UpdateButtonText();
    }

    public void ResetLevel()
    {
        _idler.LevelUp(-_idler.IdlerObject.Level);
        UpdateTextFields();
    }

    private void OnEnable()
    {
        if(_idler != null)
            UpdateTextFields();
    }
}
