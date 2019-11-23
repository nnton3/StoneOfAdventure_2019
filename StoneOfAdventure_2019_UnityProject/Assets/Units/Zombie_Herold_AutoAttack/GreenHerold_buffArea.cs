using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHerold_buffArea : BuffArea
{
    [SerializeField] private float attackspeedGainInPercent = 0.5f;
    [SerializeField] private float movespeedGainInPercent = 0.5f;

    private Dictionary<GameObject, List<BaseBuff>> buffedUnits = new Dictionary<GameObject, List<BaseBuff>>();

    protected override void AddBuffs(Collider2D collision)
    {
        var target = collision.gameObject;
        buffedUnits.Add(target, new List<BaseBuff>());
        ApplyMovespeedBuff(target);
        ApplyAttackspeedBuff(target);
    }

    private void ApplyAttackspeedBuff(GameObject target)
    {
        if (!target.GetComponent<Fighter>()) return;

        var attackspeedBuff = target.AddComponent<AttackspeedBuff>();
        buffedUnits[target].Add(attackspeedBuff);
        attackspeedBuff.Initialize(attackspeedGainInPercent);
    }

    private void ApplyMovespeedBuff(GameObject target)
    {
        if (!target.GetComponent<Mover>()) return;

        var movespeedBuff = target.AddComponent<MovespeedBuff>();
        buffedUnits[target].Add(movespeedBuff);
        movespeedBuff.Initialize(movespeedGainInPercent);
    }

    protected override void RemoveBuffs(Collider2D collision)
    {
        foreach (var buff in buffedUnits[collision.gameObject])
        {
            buff.RemoveBuff();
        }
        buffedUnits.Remove(collision.gameObject);
    }
}
