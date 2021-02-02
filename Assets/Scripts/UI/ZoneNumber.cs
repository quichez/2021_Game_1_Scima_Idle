using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneNumber : MonoBehaviour
{
    TextMeshProUGUI _text => GetComponent<TextMeshProUGUI>();

    public void UpdateZoneNumber(Zone zone)
    {
        _text.text = "Area " + zone.Area + "-" + zone.Enemy;
    }
}
