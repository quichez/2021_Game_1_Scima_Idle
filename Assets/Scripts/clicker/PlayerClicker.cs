using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerClicker : MonoBehaviour
{
    [Header("Base Damage")]
    [Range(0.000f, 9.999f)] public float DamageMantissa;
    public float DamageExponent;
    public BigNumber BaseDamage => new BigNumber(DamageMantissa, DamageExponent);

    public bool Unlocked { get; protected set; }

    protected PlayerIdlers playerIdlers => GetComponent<PlayerIdlers>();

    public void Unlock(bool unlock)
    {
        Unlocked = unlock;
    }

    public abstract string Description();
}
