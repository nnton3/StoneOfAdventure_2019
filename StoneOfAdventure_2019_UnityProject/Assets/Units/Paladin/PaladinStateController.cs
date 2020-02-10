﻿using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;
using Panda;
using StoneOfAdventure.UI;

public class PaladinStateController : Unit
{
    #region Variables
    [SerializeField] private float meleeAttackDistance = 1f;

    private Transform player;
    private PaladinFighter fighter;
    private Mover mover;
    private PaladinSkill1 skill1;
    private PaladinSkill2 skill2;
    private PaladinSkill3 skill3;
    private PaladinSkill4 skill4;
    private int attackNumber = 0;
    #endregion

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().transform;
        fighter = GetComponent<PaladinFighter>();
        mover = GetComponent<Mover>();
        skill1 = GetComponent<PaladinSkill1>();
        skill2 = GetComponent<PaladinSkill2>();
        skill3 = GetComponent<PaladinSkill3>();
        skill4 = GetComponent<PaladinSkill4>();

        GameObject.Find("BossHealth").GetComponent<HPBar_manualSetUnit>().Initialize(gameObject);
    }

    private void Update()
    {
        if (currentState == State.Death) return;
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
    public void Melee1()
    {
        switch (currentState)
        {
            case State.Idle:
                UseAttack();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                UseAttack();
                break;
        }

        void UseAttack()
        {
            StateAttack();
            attackNumber++;
            fighter.StartAttack();
            Task.current.Succeed();
        }
    }

    [Task]
    public void Melee2()
    {
        switch (currentState)
        {
            case State.Idle:
                UseAttack();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                UseAttack();
                break;
        }

        void UseAttack()
        {
            StateAttack();
            attackNumber++;
            fighter.StartMelee2();
            Task.current.Succeed();
        }
    }

    [Task]
    public void Melee3()
    {
        switch (currentState)
        {
            case State.Idle:
                UseAttack();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                UseAttack();
                break;
        }

        void UseAttack()
        {
            StateAttack();
            attackNumber++;
            fighter.StartMelee3();
            Task.current.Succeed();
        }
    }

    [Task]
    public void RangeAttack()
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
            //if (!skill1.CanUseSkill) return;
            skill1.StartUse();
            StateAttack();
            Task.current.Succeed();
        }
    }

    [Task]
    public void Meteor()
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
            //if (!skill2.CanUseSkill) return;
            skill2.StartUse();
            StateAttack();
            Task.current.Succeed();
        }
    }

    [Task]
    public void JumpInMelee()
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
            //if (!skill3.CanUseSkill) return;
            skill3.TeleportInRange = false;
            skill3.StartUse();
            StateAttack();
            Task.current.Succeed();
        }
    }

    [Task]
    public void JumpInRange()
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
            //if (!skill3.CanUseSkill) return;
            skill3.TeleportInRange = true;
            skill3.StartUse();
            StateAttack();
            Task.current.Succeed();
        }
    }

    [Task]
    public void Curse()
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
            Task.current.Succeed();
        }
    }

    public override void DisableState()
    {
        SetState(State.Idle);
    }

    // Animation event
    [Task]
    public void EndAttack()
    {
        Task.current.Succeed();
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


    [Task]
    private void GoInMelee()
    {
        MoveHorizontal(CalculateDirection());
        if (CanAttackInMelee()) Task.current.Succeed();
    }

    [Task]
    private bool CanAttackInMelee()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= meleeAttackDistance;
    }

    public float CalculateDirection()
    {
        return Mathf.Sign(player.transform.position.x - transform.position.x);
    }

    [Task]
    public bool CompareAttackNumber(int attackNumber)
    {
        return this.attackNumber >= attackNumber;
    }

    [Task]
    public void ResetAttackEnumerator()
    {
        attackNumber = 0;
        Task.current.Succeed();
    }
}
