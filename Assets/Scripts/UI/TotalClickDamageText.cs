using UnityEngine;
using TMPro;

public class TotalClickDamageText : MonoBehaviour
{
    [SerializeField] PlayerIdlers _player;
    TextMeshProUGUI text => GetComponent<TextMeshProUGUI>();

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
        _player.OnPlayerDamageUpdateCallback += UpdateText;
    }

    private void OnDisable()
    {
        _player.OnPlayerDamageUpdateCallback -= UpdateText;
    }

    public void UpdateText() => text.text = "Click Damage: " + BigNumber.Zero.ToString();
}
