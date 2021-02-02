using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public GameObject airEffect;
    public GameObject earthEffect;
    
    public void InstantiateEffect()
    {
        if (Player.Instance.GetComponent<AirClicker>().Unlocked)
        {
            GameObject clone = Instantiate(airEffect, transform);
            Destroy(clone, 0.75f);
        }
        if (Player.Instance.GetComponent<EarthClicker>().Unlocked)
        {
            GameObject clone = Instantiate(earthEffect, transform);
            Destroy(clone, 1.0f);
        }
    }
}
