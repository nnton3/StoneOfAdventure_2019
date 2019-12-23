using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinStateController : Unit
{
    private Transform player;
    private Fighter fighter;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().transform;
        fighter = GetComponent<Fighter>();
    }

    private void Update()
    {
        if (currentState == State.Death) return;
        if (player) Attack();
    }

    #region Events
    public override void Attack()
    {
        if (currentState == State.Idle)
        {
            StateAttack();
            fighter.StartAttack();
        }
    }
    #endregion

    #region Transitions
    private void StateAttack()
    {
        SetState(State.Attack);
    }
    #endregion

    private void SetState(State value)
    {
        currentState = value;
    }
}
