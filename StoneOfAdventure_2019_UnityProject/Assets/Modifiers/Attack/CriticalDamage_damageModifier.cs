using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class CriticalDamage_damageModifier : MonoBehaviour
{
    private Fighter fighter;
    private float addedDamageInPercent = 0.5f;
    private float criticalChance = 50f;

    public void Initialize(float _damageScale, float _criticalChance)
    {
        addedDamageInPercent = _damageScale;
        criticalChance = _criticalChance;
    }

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        
        fighter.AddModifierOfDamage(CalculateDamageScale);
    }

    private void CalculateDamageScale(ref float damage)
    {
        float chance = Random.Range(0f, 100f);
        if (chance <= criticalChance)
        {
            damage += fighter.BaseDamage * addedDamageInPercent;
        }
    }
}
