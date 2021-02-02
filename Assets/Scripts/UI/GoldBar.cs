using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldBar : MonoBehaviour
{
    private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();

    private void Start()
    {
        Player.Instance.OnGoldUpdateCallback += UpdateText;
        UpdateText();
    }

    public void UpdateText() => _text.text = Player.Instance.Gold.ToString();

    private void OnDisable()
    {
        Player.Instance.OnGoldUpdateCallback -= UpdateText;
    }
}
