using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamagePopUpController : MonoBehaviour
{
    public static TestDamagePopUpController Instance;
    private static bool IsInitialized => Instance != null;

    [SerializeField] private TestDamagePopUpText damagePopUp;

    private void Awake()
    {
        if (IsInitialized)
            return;
        Instance = this;
    }

    public void PopUpDamage(BigNumber dmg)
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10;        
        TestDamagePopUpText dmgPopUp = Instantiate(damagePopUp, transform.position, Quaternion.identity);
        dmgPopUp.Setup(dmg);
    }
}
