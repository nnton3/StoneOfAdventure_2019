using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinStateController : Unit
{
    #region Variables
    private Transform player;
    private Fighter fighter;
    private Mover mover;
    private ChaseBehaviour chaseBehaviour;
    private PaladinSkill1 skill1;
    private PaladinSkill2 skill2;
    #endregion

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().transform;
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();
        skill1 = GetComponent<PaladinSkill1>();
        skill2 = GetComponent<PaladinSkill2>();
    }

    private void Update()
    {
        if (currentState == State.Death) return;

        chaseBehaviour.UpdateChaseBehaviour();
        MoveHorizontal(chaseBehaviour.CalculateDirection());
    }

    #region Events
    public override void MoveHorizontal(float direction)
    {
        switch (currentState)
        {
            case State.Idle:
                if (direction != 0f) StateMoveHorizontal();
                break;
            case State.MoveHorizontal:
                if (direction == 0f) StateIdle();
                else mover.MoveTo(direction);
                break;
        }
    }

    public override void Attack()
    {
        switch (currentState)
        {
            case State.Idle:
                StateAttack();
                fighter.StartAttack();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                StateAttack();
                fighter.StartAttack();
                break;
        }
    }

    public override void Skill1()
    {
        switch (currentState)
        {
            case State.Idle:
                TryToUseSkill();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                TryToUseSkill();
                break;
        }

        void TryToUseSkill()
        {
            if (!skill1.CanUseSkill) return;
            skill1.StartUse();
            StateAttack();
        }
    }

    public override void Skill2()
    {
        switch (currentState)
        {
            case State.Idle:
                TryToUseSkill();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                TryToUseSkill();
                break;
        }

        void TryToUseSkill()
        {
            if (!skill2.CanUseSkill) return;
            skill2.StartUse();
            StateAttack();
        }
    }

    public override void DisableState()
    {
        SetState(State.Idle);
    }
    #endregion

    #region Transitions
    private void StateIdle()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        DisableState();
    }

    private void StateMoveHorizontal()
    {
        SetState(State.MoveHorizontal);
    }

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
