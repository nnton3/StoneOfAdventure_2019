using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;
using Panda;

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

    [Task]
    public void Melee2()
    {
        switch (currentState)
        {
            case State.Idle:
                StateAttack();
                fighter.StartMelee2();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                StateAttack();
                fighter.StartMelee2();
                break;
        }
    }

    [Task]
    public void Melee3()
    {
        switch (currentState)
        {
            case State.Idle:
                StateAttack();
                fighter.StartMelee3();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                StateAttack();
                fighter.StartMelee3();
                break;
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
            if (!skill1.CanUseSkill) return;
            skill1.StartUse();
            StateAttack();
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
            if (!skill2.CanUseSkill) return;
            skill2.StartUse();
            StateAttack();
        }
    }

    [Task]
    public void FireJump()
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
        }
    }

    public override void DisableState()
    {
        SetState(State.Idle);
    }

    public void SucceedTask()
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
}
