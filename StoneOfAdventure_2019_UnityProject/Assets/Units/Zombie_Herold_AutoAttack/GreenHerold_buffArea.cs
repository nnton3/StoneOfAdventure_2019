using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHerold_buffArea : BuffArea
{
    [SerializeField] private float attackspeedGainInPercent = 0.5f;
    [SerializeField] private float movespeedGainInPercent = 0.5f;

    private List<BaseBuff> appliedBuffs = new List<BaseBuff>();

    protected override void AddBuffs(Collider2D collision)
    {
        var target = collision.gameObject;
        ApplyMovespeedBuff(target);
        ApplyAttackspeedBuff(target);
    }

    private void ApplyAttackspeedBuff(GameObject target)
    {
        if (!target.GetComponent<Fighter>()) return;

        var attackspeedBuff = target.AddComponent<AttackspeedBuff>();
        appliedBuffs.Add(attackspeedBuff);
        attackspeedBuff.Initialize(attackspeedGainInPercent);
    }

    private void ApplyMovespeedBuff(GameObject target)
    {
        if (!target.GetComponent<Mover>()) return;

        var movespeedBuff = target.AddComponent<MovespeedBuff>();
        appliedBuffs.Add(movespeedBuff);
        movespeedBuff.Initialize(movespeedGainInPercent);
    }

    protected override void RemoveBuffs(Collider2D collision)
    {
        foreach (var buff in appliedBuffs)
        {
            buff.RemoveBuff();
            appliedBuffs.Remove(buff);
        }
    }
}
