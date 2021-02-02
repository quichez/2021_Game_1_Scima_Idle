using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalDamageText : MonoBehaviour
{
    public TextMeshProUGUI text => GetComponent<TextMeshProUGUI>();
    
    public void UpdateText()
    {
        text.text = "DPS: " + Player.Instance.TotalIdlerDamage().ToString() + "\n" + "MPS: " + Player.Instance.TotalIdlerManaCost().ToString();
    }
    
    void Update()
    {
        UpdateText();
    }
}
