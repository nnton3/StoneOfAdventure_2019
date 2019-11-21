using UnityEngine;
using System.Collections;

public class SphereOfEnergy_Artifact : Artifact
{
    [SerializeField] private float targetTimeOnFeet = 2f;
    [SerializeField] private float bonusDamageInPercent = 1.5f;

    public void AddGainPower()
    {
        SphereOfEnergy_damageModifier ciriticalDamage = player.AddComponent<SphereOfEnergy_damageModifier>();
        ciriticalDamage.Initialize(targetTimeOnFeet, bonusDamageInPercent);
        Destroy(gameObject);
    }
}
