﻿using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class ZombieAttackState : BaseState
{
    #region Variables
    private Animator anim;
    private ZombieStateController unit;
    private Fighter fighter;
    private Stunned stunned;

    private BaseState deathState;
    private BaseState stunState;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        fighter = GetComponent<Fighter>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public override void Stun(float time)
    {
        fighter.Cancel();
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
