﻿using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class ZombieAttackState : MonoBehaviour, IZombieState
{
    #region Variables
    private Animator anim;
    private ZombieStateController unit;
    private Fighter fighter;
    private Stunned stunned;

    private ZombieDeathState deathState;
    private ZombieStunState stunState;
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

    public void Attack() { return; }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void Stun(float time)
    {
        fighter.Cancel();
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
