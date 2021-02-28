using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public static EffectSpawner Instance;
    private static bool IsInitialized => Instance != null;
    public GameObject[] effects = new GameObject[7];

    private void Awake()
    {
        if (IsInitialized)
            return;
        Instance = this;
    }


    public GameObject InstantiateEffect(GameObject effect)
    {         
        return Instantiate(effect, transform);
    }
}
