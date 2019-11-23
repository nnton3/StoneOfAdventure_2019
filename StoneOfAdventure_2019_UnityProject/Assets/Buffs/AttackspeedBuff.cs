using System;
using StoneOfAdventure.Combat;
using UnityEngine;

public class AttackspeedBuff : BaseBuff
{
    private float attackspeedGain = 0.5f;
    private Fighter fighter;

    internal void Initialize(float attackspeedGainInPercent)
    {
        attackspeedGain = attackspeedGainInPercent;
    }

    private void Awake()
    {
        fighter = GetComponent<Fighter>();
        ApplyBuff();
    }

    public override void ApplyBuff()
    {
        fighter.ModifyAttackSpeed(attackspeedGain);
    }

    public override void RemoveBuff()
    {
        fighter.ModifyAttackSpeed(-attackspeedGain);
        Destroy(this);
    }
}
