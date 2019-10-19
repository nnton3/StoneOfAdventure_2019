﻿using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class ZombieIdleState : MonoBehaviour, IZombieState
{
    private Animator anim;
    private ZombieStateController unit;
    private Fighter fighter;
    private Mover mover;
    private Stunned stunned;

    private ZombieAttackState attackState;
    private ZombieHorizontalMoveState moveHorizontalState;
    private ZombieDeathState deathState;
    private ZombieStunState stunState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        stunned = GetComponent<Stunned>();

        attackState = GetComponent<ZombieAttackState>();
        moveHorizontalState = GetComponent<ZombieHorizontalMoveState>();
        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
    }

    public void Attack()
    {
        unit.State = attackState;
        fighter.StartAttack();
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        unit.State = moveHorizontalState;
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void Stun(float time)
    {
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
