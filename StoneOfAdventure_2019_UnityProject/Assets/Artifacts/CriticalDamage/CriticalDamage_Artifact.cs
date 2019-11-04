using UnityEngine;
using System.Collections;

public class CriticalDamage_Artifact : Artifact
{
    [SerializeField] private float criticalChance = 50f;
    [SerializeField] private float damageScale = 1.5f;

    public void AddCriicalDamage()
    {
        CriticalDamage_AttackModifier ciriticalDamage = player.AddComponent<CriticalDamage_AttackModifier>();
        ciriticalDamage.CriticalChance = criticalChance;
        ciriticalDamage.DamageScale = damageScale;
        Destroy(gameObject);
    }
}
