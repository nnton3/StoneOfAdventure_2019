using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class PaladinStateController : Unit
{
    #region Variables
    private Transform player;
    private Fighter fighter;
    private Mover mover;
    private ChaseBehaviour chaseBehaviour;
    private PaladinSkill1 skill1;
    private PaladinSkill2 skill2;
    private PaladinSkill3 skill3;
    private PaladinSkill4 skill4;
    #endregion

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().transform;
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();
        skill1 = GetComponent<PaladinSkill1>();
        skill2 = GetComponent<PaladinSkill2>();
        skill3 = GetComponent<PaladinSkill3>();
        skill4 = GetComponent<PaladinSkill4>();
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

    [Task]
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
        Task.current.Succeed();
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

    public void Skill3()
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
            if (!skill3.CanUseSkill) return;
            skill3.StartUse();
            StateAttack();
        }
    }

    public void Skill4()
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
            if (!skill4.CanUseSkill) return;
            skill4.StartUse();
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
