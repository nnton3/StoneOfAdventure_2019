using UnityEngine;
using System.Collections;

public class CriticalDamage_Artifact : Artifact
{
    [SerializeField] private float criticalChance = 50f;
    [SerializeField] private float damageScale = 1.5f;

    public void AddCriicalDamage()
    {
        var ciriticalDamage = player.AddComponent<CriticalDamage_AttackModifier>();
        ciriticalDamage.Initialize(damageScale, criticalChance);
        Destroy(gameObject);
    }
}
