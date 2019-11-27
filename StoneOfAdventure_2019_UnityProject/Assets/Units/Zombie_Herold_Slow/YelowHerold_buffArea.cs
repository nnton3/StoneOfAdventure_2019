using StoneOfAdventure.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YelowHerold_buffArea : BuffArea
{
    [SerializeField] private float slowInPercent = 0.5f;
    [SerializeField] private float actionTime = 3f;

    protected override void AddBuffs(GameObject target)
    {
        buffedUnits.Add(target, new List<BaseBuff>());
        ApplySlowDownAttack(target);
    }

    private void ApplySlowDownAttack(GameObject target)
    {
        if (!target.GetComponent<Fighter>()) return;

        var slowDownAttack = target.AddComponent<SlowDown_effect>();
        buffedUnits[target].Add(slowDownAttack);
        slowDownAttack.Initialize(slowInPercent, actionTime);
        slowDownAttack.ApplyBuff();
        AddEffect(target);
    }
}
