using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaBar : MonoBehaviour
{
    private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();

    private void Start()
    {
        Player.Instance.OnManaUpdateCallback += UpdateText;
        UpdateText();
    }

    public void UpdateText() => _text.text = Player.Instance.Mana.Rounded.ToString();

}
