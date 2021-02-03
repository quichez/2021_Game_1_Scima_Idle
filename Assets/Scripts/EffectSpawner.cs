using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public GameObject[] effects = new GameObject[7];
    
    public void InstantiateEffect()
    {
        if (Player.Instance.GetComponent<AirClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[0], transform);
            Destroy(clone, 0.75f);
        }
        if (Player.Instance.GetComponent<EarthClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[1], transform);
            Destroy(clone, 1.0f);
        }
        if(Player.Instance.GetComponent<WaterClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[2], transform);
        }
        if (Player.Instance.GetComponent<FireClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[3], transform);
        }
        if (Player.Instance.GetComponent<LightningClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[4], transform);
        }
        if (Player.Instance.GetComponent<DarkClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[5], transform);
        }
        if (Player.Instance.GetComponent<LightClicker>().Unlocked)
        {
            GameObject clone = Instantiate(effects[6], transform);
        }
    }
}
