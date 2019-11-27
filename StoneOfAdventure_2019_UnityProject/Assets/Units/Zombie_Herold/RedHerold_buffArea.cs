using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StoneOfAdventure.Combat;

public class RedHerold_buffArea : BuffArea
{
    [SerializeField] private float healValue = 1f;
    [SerializeField] private float periodicity = 1f;

    protected override void AddBuffs(GameObject target)
    {
        buffedUnits.Add(target, new List<BaseBuff>());
        ApplyRegenBuff(target);
        AddEffect(target);
    }

    private void ApplyRegenBuff(GameObject target)
    {
        if (!target.GetComponent<Health>()) return;

        var regenBuff = target.AddComponent<RegenBuff>();
        buffedUnits[target].Add(regenBuff);
        regenBuff.Initialize(healValue, periodicity);
        regenBuff.ApplyBuff();
    }
}
