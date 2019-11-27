using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHerold_buffArea : BuffArea
{
    [SerializeField] private float attackspeedGainInPercent = 0.5f;
    [SerializeField] private float movespeedGainInPercent = 0.5f;

    protected override void AddBuffs(GameObject target)
    {
        buffedUnits.Add(target, new List<BaseBuff>());
        ApplyMovespeedBuff(target);
        ApplyAttackspeedBuff(target);
        AddEffect(target);
    }

    private void ApplyAttackspeedBuff(GameObject target)
    {
        if (!target.GetComponent<Fighter>()) return;

        var attackspeedBuff = target.AddComponent<AttackspeedBuff>();
        buffedUnits[target].Add(attackspeedBuff);
        attackspeedBuff.Initialize(attackspeedGainInPercent);
        attackspeedBuff.ApplyBuff();
    }

    private void ApplyMovespeedBuff(GameObject target)
    {
        if (!target.GetComponent<Mover>()) return;

        var movespeedBuff = target.AddComponent<MovespeedBuff>();
        buffedUnits[target].Add(movespeedBuff);
        movespeedBuff.Initialize(movespeedGainInPercent);
        movespeedBuff.ApplyBuff();
    }
}
