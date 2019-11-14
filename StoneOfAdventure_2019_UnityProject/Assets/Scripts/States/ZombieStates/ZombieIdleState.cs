﻿using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class ZombieIdleState : BaseState
{
    #region Variables
    private Animator anim;
    private Unit unit;
    private Fighter fighter;
    private Mover mover;
    private Stunned stunned;

    private BaseState attackState;
    private BaseState moveHorizontalState;
    private BaseState deathState;
    private BaseState stunState;
    private BaseState inTheAirState;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        stunned = GetComponent<Stunned>();

        attackState = GetComponent<ZombieAttackState>();
        moveHorizontalState = GetComponent<ZombieHorizontalMoveState>();
        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
        inTheAirState = GetComponent<ZombieInTheAirState>();
    }

    public override void Attack()
    {
        unit._State = attackState;
        fighter.StartAttack();
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        unit._State = moveHorizontalState;
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        unit._State = deathState;
    }

    public override void Stun(float time)
    {
        unit._State = stunState;
        stunned.ApplyStun(time);
    }

    public override void Fell()
    {
        unit._State = inTheAirState;
    }
}
