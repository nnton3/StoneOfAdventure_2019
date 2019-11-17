using UnityEngine;
using System.Collections;

public class SphereOfEnergy_Artifact : Artifact
{
    [SerializeField] private float targetTimeOnFeet = 2f;
    [SerializeField] private float bonusDamageInPercent = 1.5f;

    public void AddGainPower()
    {
        SphereOfEnergy_AttackModifier ciriticalDamage = player.AddComponent<SphereOfEnergy_AttackModifier>();
        ciriticalDamage.targetTimeOnFeet = targetTimeOnFeet;
        ciriticalDamage.bonusDamageInPercent = bonusDamageInPercent;
        Destroy(gameObject);
    }
}
