using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using System;
using StoneOfAdventure.Movement;

public class ZombieStateController : UnitContainsAward
{
    #region Variables
    private EnemyDetector enemyDetector;
    private GameObject currentTarget;
    private Mover mover;
    private Fighter fighter;
    private Animator anim;
    private Stunned stunned;

    private PatrolBehaviour patrolBehaviour;
    private ChaseBehaviour chaseBehaviour;

    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float patrolMovespeed = 1.5f;
    #endregion

    protected override void Start()
    {
        base.Start();

        currentState = State.Death;

        enemyDetector = GetComponentInChildren<EnemyDetector>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        anim = GetComponent<Animator>();
        stunned = GetComponent<Stunned>();

        enemyDetector.PlayerDetected.AddListener(UpdateTarget);
        enemyDetector.PlayerLost.AddListener(UpdateTarget);
    }

    private void Update()
    {
        if (currentState == State.Death) return;
        if (currentState == State.InTheAir)
        {
            MoveHorizontal(0f, patrolMovespeed);
            return;
        }
        if (currentTarget)
        {
            chaseBehaviour.UpdateChaseBegaviour();
            MoveHorizontal(chaseBehaviour.CalculateDirection(), movespeed);
        }
        else
        {
            patrolBehaviour.UpdatePatrolBehaviour();
            MoveHorizontal(patrolBehaviour.PatrolDirection, patrolMovespeed);
        }
    }

    public void UpdateTarget()
    {
        currentTarget = enemyDetector.Player;
    }

    #region Events
    private void MoveHorizontal(float direction, float movespeed)
    {
        switch (currentState)
        {
            case State.Idle:
                if (direction != 0f) StateMoveHorizontal();
                break;
            case State.MoveHorizontal:
                if (direction == 0f) StateIdle();
                else mover.MoveTo(direction, movespeed);
                break;
            case State.InTheAir:
                mover.MoveInAirTo(direction, movespeed);
                break;
        }
    }

    public override void Attack()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                StateAttack();
                fighter.StartAttack();
                break;
        }
    }

    public override void DisableState() => StateIdle();

    public override void Dead()
    {
        switch (currentState)
        {
            case State.Idle:
                anim.SetTrigger("dead");
                StateDeath();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                anim.SetTrigger("dead");
                StateDeath();
                break;
            case State.InTheAir:
                mover.CancelMove();
                anim.SetTrigger("dead");
                StateDeath();
                break;
            case State.Attack:
                fighter.CancelAttack();
                anim.SetTrigger("dead");
                StateDeath();
                break;
            case State.Stun:
                anim.SetTrigger("dead");
                StateDeath();
                break;
        }

        enemyDetector.PlayerDetected.RemoveAllListeners();
        enemyDetector.PlayerLost.RemoveAllListeners();
        StartCoroutine("DestroyCorrupse");
        CreateReward();
    }

    public override void Landed()
    {
        if (currentState == State.Death) return;
        DisableState();
    }

    public override void Born()
    {
        if (currentState == State.Death) DisableState();
    }

    public override void ApplyStun(float timeOfStun)
    {
        switch (currentState)
        {
            case State.Idle:
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
            case State.Attack:
                fighter.CancelAttack();
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
        }
    }
    #endregion

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    #region StateTransitions
    private void StateIdle()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.Idle);
    }

    private void StateMoveHorizontal()
    {
        SetState(State.MoveHorizontal);
    }

    private void StateAttack()
    {
        SetState(State.Attack);
    }

    private void StateDeath()
    {
        SetState(State.Death);
    }

    private void StateStun()
    {
        SetState(State.Stun);
    }

    #endregion

    private void SetState(State value)
    {
        currentState = value;
    }
}
